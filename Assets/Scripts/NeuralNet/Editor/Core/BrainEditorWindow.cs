using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class BrainEditorWindow : EditorWindowSingleton<BrainEditorWindow>
    {
        protected override string windowTitle { get; } = "Brain Editor";
        private List<Node> nodes;
        private List<Connection> connections;
        private ConnectionPoint selectedInPoint;
        private ConnectionPoint selectedOutPoint;

        
        [MenuItem("Window/Brain Editor")]
        private static void OpenWindow()
        {
            var window = Instance;
        }
        
        private void OnGUI()
        {
            DrawNodes();
            DrawConnections();
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
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        ProcessContextMenu(e.mousePosition);
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
 
        private void OnClickAddNode(Vector2 mousePosition)
        {
            if (nodes == null)
            {
                nodes = new List<Node>();
            }
 
            nodes.Add(new Node(mousePosition, 200, 50, OnClickInPoint, OnClickOutPoint));
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
 
            connections.Add(new Connection(selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
        }
 
        private void ClearConnectionSelection()
        {
            selectedInPoint = null;
            selectedOutPoint = null;
        }
    }
}