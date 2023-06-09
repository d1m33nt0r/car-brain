using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core;
using NeuralNet.Core.Neurons.Output;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Interfaces;
using NeuralNet.Editor.Core;
using NeuralNet.Editor.Core.ConnectionPoints;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.MiddleSection
{
    public class Graph : StylizedDrawer<EmptyArgs>, ISizeListener, IInitializable<GraphInitArgs>
    {
        private NeuralNetworkData currentNetworkAsset;
        
        private List<Node> nodes = new ();
        private List<Connection> connections = new ();
        private NodeConnectionPoint selectedInPoint;
        private NodeConnectionPoint selectedOutPoint;
        
        public float kZoomMin = 0.1f;
        public float kZoomMax = 10.0f;
        
        public float zoom = 1.0f;
        public Vector2 zoomCoordsOrigin = Vector2.zero;
        
        protected override void ApplyStyles()
        {
            
        }

        public override void Draw(EmptyArgs args)
        {
            //EditorZoomArea.Begin(zoom, rect);
            DrawConnections();
            DrawNodes();
            DrawTempConnectionLine(Event.current);
            //EditorZoomArea.End();
            
            ProcessNodeEvents(Event.current);
            ProcessEvents(Event.current);
        }

        public void OnChangedRectSize(Vector2 newSize)
        {
            rect = new Rect(rect.position, newSize);
        }
        
        private void DrawConnections()
        {
            var args = new EmptyArgs();
            
            if (connections != null)
            {
                for (var i = 0; i < connections.Count; i++)
                {
                    connections[i].Draw(args);
                } 
            }
        }
        
        private void DrawNodes()
        {
            if (nodes != null)
            {
                var args = new EmptyArgs();
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
                        ProcessContextMenu(e.mousePosition / zoom);
                    }
                    break;
                case EventType.MouseDrag:
                    if (e.button == 2)
                    {
                        OnDrag(e.delta / zoom);
                        //gridDrawer.OnDrag(e.delta / zoom);
                    }
                    break;
                case EventType.ScrollWheel:
                    /*var screenCoordsMousePos = Event.current.mousePosition;
                    var delta = Event.current.delta;
                    var zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
                    var zoomDelta = -delta.y / 150.0f;
                    var oldZoom = zoom;
                    zoom += zoomDelta;
                    zoom = Mathf.Clamp(zoom, kZoomMin, kZoomMax);
                    zoomCoordsOrigin += (zoomCoordsMousePos - zoomCoordsOrigin) -
                                              (oldZoom / zoom) * (zoomCoordsMousePos - zoomCoordsOrigin);

                    Event.current.Use();*/
                    break;
            }
        }
        
        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - BrainEditorWindow.Instance.position.TopLeft()) / zoom + zoomCoordsOrigin;
        }
        
        private void ProcessNodeEvents(Event e)
        {
            if (nodes != null)
            {
                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    bool guiChanged = nodes[i].ProcessEvents(e, e.mousePosition - rect.position, zoom);
 
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
            var neuron = currentNetworkAsset.AddNeuron(mousePosition);
            var node = new Node(neuron, mousePosition, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
            nodes.Add(node);
        }
        
        private void OnClickRemoveNode(Node node)
        {
            if (connections != null)
            {
                List<Connection> connectionsToRemove = new List<Connection>();

                for (int i = 0; i < connections.Count; i++)
                {
                    if (connections[i].inPoint == node.InNodeNodePoint || connections[i].outPoint == node.OutNodeNodePoint)
                    {
                        connectionsToRemove.Add(connections[i]);
                    }
                }
 
                for (int i = 0; i < connectionsToRemove.Count; i++)
                {
                    connections.Remove(connectionsToRemove[i]);
                    var neuronID = connectionsToRemove[i].outPoint.node.neuron.id;
                    for (var n = 0; n < currentNetworkAsset.hiddenNeurons.Count; n++)
                    {
                        for (var w = 0; w < currentNetworkAsset.hiddenNeurons[n].inputWeights.Count; w++)
                        {
                            if (currentNetworkAsset.hiddenNeurons[n].inputWeights[w].inputNeuron.id == neuronID)
                                currentNetworkAsset.hiddenNeurons[n].inputWeights
                                    .Remove(currentNetworkAsset.hiddenNeurons[n].inputWeights[w]);
                        }

                        if (currentNetworkAsset.outputNeurons.Count > 0)
                        {
                            for (var w = 0; w < currentNetworkAsset.outputNeurons[n].inputWeights.Count; w++)
                            {
                                if (currentNetworkAsset.outputNeurons[n].inputWeights[w].inputNeuron.id == neuronID)
                                    currentNetworkAsset.outputNeurons[n].inputWeights
                                        .Remove(currentNetworkAsset.outputNeurons[n].inputWeights[w]);
                            }
                        }
                    }
                    
                    currentNetworkAsset.RemoveWeight(connectionsToRemove[i].weight);
                }
            
                connectionsToRemove = null;
            }
 
            currentNetworkAsset.RemoveNeuron(node.neuron);
            nodes.Remove(node);
        }
        
        private void OnClickInPoint(NodeConnectionPoint inPoint)
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
 
        private void OnClickOutPoint(NodeConnectionPoint outPoint)
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
            var weight = currentNetworkAsset.AddWeight(selectedOutPoint.node.neuron, selectedInPoint.node.neuron);
            var connection = new Connection(weight, selectedInPoint, selectedOutPoint, OnClickRemoveConnection);
            connections.Add(connection);
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

        private void SynchronizeNetwork(NeuralNetworkData networkData)
        {
            currentNetworkAsset = networkData;
            nodes.Clear();
            connections.Clear();

            for (var i = 0; i < networkData.inputNeurons.Count; i++)
            {
                var neuron = networkData.inputNeurons[i];
                var node = new Node(neuron,
                    neuron.position, OnClickInPoint, OnClickOutPoint,
                    OnClickRemoveNode);
                nodes.Add(node);
            }

            for (var i = 0; i < networkData.hiddenNeurons.Count; i++)
            {
                var neuron = networkData.hiddenNeurons[i];
                var node = new Node(neuron,
                    neuron.position, OnClickInPoint, OnClickOutPoint,
                    OnClickRemoveNode);
                nodes.Add(node);
            }

            for (var i = 0; i < networkData.outputNeurons.Count; i++)
            {
                var neuron = networkData.outputNeurons[i];
                var node = new Node(neuron,
                    neuron.position, OnClickInPoint, OnClickOutPoint,
                    OnClickRemoveNode);
                nodes.Add(node);
            }

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var neuron = nodes[i].neuron;
                for (var j = 0; j < neuron.inputWeights.Count; j++)
                {
                    Neuron inputNeuron = null;
                    for (var k = 0; k < networkData.outputNeurons.Count; k++)
                    {
                        var isNeededNeuron = neuron.inputWeights[j].inputNeuronID == networkData.outputNeurons[k].id;
                        if (isNeededNeuron)
                        {
                            inputNeuron = networkData.outputNeurons[k];
                        }
                    }

                    for (var k = 0; k < networkData.hiddenNeurons.Count; k++)
                    {
                        var isNeededNeuron = neuron.inputWeights[j].inputNeuronID == networkData.hiddenNeurons[k].id;
                        if (isNeededNeuron)
                        {
                            inputNeuron = networkData.hiddenNeurons[k];
                        }
                    }

                    for (var k = 0; k < networkData.inputNeurons.Count; k++)
                    {
                        var isNeededNeuron = neuron.inputWeights[j].inputNeuronID == networkData.inputNeurons[k].id;
                        if (isNeededNeuron)
                        {
                            inputNeuron = networkData.inputNeurons[k];
                        }
                    }

                    neuron.inputWeights[j].AttachNeurons(inputNeuron, neuron);
                    var inPoint = neuron.inputWeights[j].inputNeuron.id;
                    var res = nodes.FirstOrDefault(n => n.neuron.id == inPoint);
                    var connection = new Connection(neuron.inputWeights[j], node.InNodeNodePoint, res.OutNodeNodePoint,
                        OnClickRemoveConnection);
                    connections.Add(connection);
                }
            }
        }

        public void Initialize(GraphInitArgs args)
        {
            args.State.onChangeNetworkAsset += SynchronizeNetwork;
        }
    }
}