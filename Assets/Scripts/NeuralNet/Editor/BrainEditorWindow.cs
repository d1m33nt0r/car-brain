using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core;
using NeuralNet.Editor.Template;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class BrainEditorWindow : EditorWindowSingleton<BrainEditorWindow>
    {
        public GlobalState State { get; private set; }
        
        protected override string windowTitle { get; } = "Brain Editor";
        private List<Node> nodes = new ();
        private List<Connection> connections = new ();
        private ConnectionPoint selectedInPoint;
        private ConnectionPoint selectedOutPoint;
        private Vector2 drag;

        private TopMenu topMenu;

        [MenuItem("Window/Brain Editor")]
        private static void OpenWindow()
        {
            var window = Instance;
        }

        private void OnEnable()
        {
            topMenu = new TopMenu();
            topMenu.OnChangedCurrentNetworkAsset += SynchronizeNetwork;
            State = new GlobalState { CurrentNetworkAsset = new NeuralNetworkData() };
        }

        private void OnGUI()
        {
            DrawNodes();
            DrawConnections();
            DrawTempConnectionLine(Event.current);
            
            topMenu.Draw(new EmptyDrawerArgs());
            
            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);

            if (GUI.changed) Repaint();
        }

        private void DrawConnections()
        {
            if (connections != null)
            {
                for (var i = 0; i < connections.Count; i++)
                {
                    connections[i].Draw();
                } 
            }
        }
        
        private void DrawNodes()
        {
            if (nodes != null)
            {
                var args = new EmptyDrawerArgs();
                for (var i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Draw(args);
                }
            }
        }

        private void ProcessEvents(Event e)
        {
            drag = Vector2.zero;            
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 2)
                    {
                        OnDrag(e.delta);
                    }
                    break;
            }
        }
        
        private void ProcessNodeEvents(Event e)
        {
            if (nodes != null)
            {
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged = nodes[i].ProcessEvents(e);
 
                    if (guiChanged)
                    {
                        GUI.changed = true;
                    }
                }
            }
        }
        
        private void ProcessContextMenu(Vector2 mousePosition)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition)); 
            genericMenu.ShowAsContext();
        }
 
        private void OnDrag(Vector2 delta)
        {
            drag = delta;
 
            if (nodes != null)
            {
                for (var i = 0; i < nodes.Count; i++)
                {
                    nodes[i].Drag(delta);
                }
            }
 
            GUI.changed = true;
        }
        
        private void OnClickAddNode(Vector2 mousePosition)
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }
            var neuron = State.CurrentNetworkAsset.AddNeuron(mousePosition);
            nodes.Add(new Node(neuron, mousePosition, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode));
        }
        
        private void OnClickRemoveNode(Node node)
        {
            if (connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();
 
                for (int i = 0; i < connections.Count; i++)
                {
                    if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
                    {
                        connectionsToRemove.Add(connections[i]);
                    }
                }
 
                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    connections.Remove(connectionsToRemove[i]);
                }
 
                connectionsToRemove = null;
            }
 
            nodes.Remove(node);
        }
        
        private void OnClickInPoint(ConnectionPoint inPoint)
        {
            selectedInPoint = inPoint;
 
            if (selectedOutPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection(); 
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }
 
        private void OnClickOutPoint(ConnectionPoint outPoint)
        {
            selectedOutPoint = outPoint;
 
            if (selectedInPoint != null)
            {
                if (selectedOutPoint.node != selectedInPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }
 
        private void OnClickRemoveConnection(Connection connection)
        {
            connections.Remove(connection);
        }
 
        private void CreateConnection()
        {
            if (connections == null)
            {
                connections = new List<Connection>();
            }
            var weight = State.CurrentNetworkAsset.AddWeight(selectedOutPoint.node.neuron, selectedInPoint.node.neuron);
            connections.Add(new Connection(weight, selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
        }
 
        private void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }
        
        private void DrawTempConnectionLine(Event e)
        {
            if (selectedInPoint != null && selectedOutPoint == null)
            {
                Handles.DrawBezier(
                    selectedInPoint.rect.center,
                    e.mousePosition,
                    selectedInPoint.rect.center + Vector2.left * 50f,
                    e.mousePosition - Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );
 
                GUI.changed = true;
            }
 
            if (selectedOutPoint != null && selectedInPoint == null)
            {
                Handles.DrawBezier(
                    selectedOutPoint.rect.center,
                    e.mousePosition,
                    selectedOutPoint.rect.center - Vector2.left * 50f,
                    e.mousePosition + Vector2.left * 50f,
                    Color.white,
                    null,
                    2f
                );
 
                GUI.changed = true;
            }
        }
        
        private void SynchronizeNetwork()
        {
            nodes.Clear();
            connections.Clear();

            for (var i = 0; i < State.CurrentNetworkAsset.allNeurons.Count; i++)
            {
                var neuron = State.CurrentNetworkAsset.allNeurons[i];
                var node = new Node(neuron,
                    neuron.position, OnClickInPoint, OnClickOutPoint,
                    OnClickRemoveNode);
                nodes.Add(node);
                for (var j = 0; j < neuron.inputWeights.Count; j++)
                {
                    var inPoint = neuron.inputWeights[j].inputNeuron.id;
                    var res = nodes.FirstOrDefault(n => n.neuron.id == inPoint);
                    var connection = new Connection(neuron.inputWeights[j], node.inPoint, res.outPoint, OnClickRemoveConnection);
                    connections.Add(connection);
                }
            }
        }
    }
}