using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Connections;
using NeuralNet.Editor.Nodes.BaseNode;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class GraphController : BaseController<GraphDrawer, GraphModel>
    {
        public event Action<Vector2> OnDragEvent;

        public override void AttachDrawer(GraphDrawer view)
        {
            base.AttachDrawer(view);
            OnDragEvent += view.OnDragCallback;
        }

        public void ProcessEvents(Event e)
        {
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
                /*case EventType.ScrollWheel:
                    var screenCoordsMousePos = Event.current.mousePosition;
                    var delta = Event.current.delta;
                    var zoomCoordsMousePos = ConvertScreenCoordsToZoomCoords(screenCoordsMousePos);
                    var zoomDelta = -delta.y / 150.0f;
                    var oldZoom = model.zoom;
                    model.zoom += zoomDelta;
                    model.zoom = Mathf.Clamp(model.zoom, model.kZoomMin, model.kZoomMax);
                    model.zoomCoordsOrigin += (zoomCoordsMousePos - model.zoomCoordsOrigin) -
                                              (oldZoom / model.zoom) * (zoomCoordsMousePos - model.zoomCoordsOrigin);

                    Event.current.Use();
                    break;*/
            }
        }

        public void ProcessNodesEvents(Event e)
        {
            for (var i = model.NodesControllers.Count - 1; i >= 0; i--)
            {
                var guiChanged = model.NodesControllers[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }

        private Vector2 ConvertScreenCoordsToZoomCoords(Vector2 screenCoords)
        {
            return (screenCoords - view.Rect.TopLeft()) / model.zoom + model.zoomCoordsOrigin;
        }

        private void ProcessContextMenu(Vector2 mousePosition)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
            genericMenu.ShowAsContext();
        }

        private void OnClickAddNode(Vector2 mousePosition)
        {
            var nodeTitle =
                $"{Constants.General.DEFAULT_NODE_TITLE}{Constants.General.NODE_SUFFIX}{model.NodesModels.Count}";
            var nodeModel = new BaseNodeModel(mousePosition,
                Constants.General.DEFAULT_NODE_SIZE.width,
                Constants.General.DEFAULT_NODE_SIZE.height,
                nodeTitle, OnClickInPoint, OnClickOutPoint);
            var nodeDrawer = new BaseNodeDrawer();
            var nodeController = new BaseNodeController();
            nodeController.OnRemoveNode += OnClickRemoveNode;
            nodeController.AttachModel(nodeModel);
            nodeController.AttachDrawer(nodeDrawer);

            model.NodesModels.Add(nodeModel);
            model.NodesDrawers.Add(nodeDrawer);
            model.NodesControllers.Add(nodeController);
        }

        private void OnClickInPoint(BaseConnectionPointController controller)
        {
            model.SelectedInPointController = controller;

            if (model.SelectedOutPointController != null)
            {
                if (model.SelectedOutPointController.model.BaseNodeModel !=
                    model.SelectedInPointController.model.BaseNodeModel)
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

        private void OnClickOutPoint(BaseConnectionPointController controller)
        {
            model.SelectedOutPointController = controller;

            if (model.SelectedInPointController != null)
            {
                if (model.SelectedOutPointController.model.BaseNodeModel !=
                    model.SelectedInPointController.model.BaseNodeModel)
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

        private void OnClickRemoveConnection(BaseConnectionController connectionController)
        {
            model.ConnectionDrawers.Remove(connectionController.view);
            model.ConnectionModels.Remove(connectionController.model);
            model.ConnectionControllers.Remove(connectionController);
        }

        private void CreateConnection()
        {
            model.ConnectionModels.Add(new BaseConnectionModel(model.SelectedInPointController,
                model.SelectedOutPointController));
            model.ConnectionDrawers.Add(new BaseConnectionDrawer());
            model.ConnectionControllers.Add(new BaseConnectionController());
            model.ConnectionControllers.Last().AttachDrawer(model.ConnectionDrawers.Last());
            model.ConnectionControllers.Last().AttachModel(model.ConnectionModels.Last());
            model.ConnectionControllers.Last().OnRemoveConnectionClicked += OnClickRemoveConnection;
        }

        private void ClearConnectionSelection()
        {
            model.SelectedInPointController = null;
            model.SelectedOutPointController = null;
        }

        private void OnClickRemoveNode(BaseNodeController node)
        {
            if (model.ConnectionControllers != null)
            {
                var connectionsToRemove = new List<BaseConnectionController>();

                for (var i = 0; i < model.ConnectionControllers.Count; i++)
                {
                    if (model.ConnectionControllers[i].model.inPointController == node.model.inPointController ||
                        model.ConnectionControllers[i].model.outPointController == node.model.outPointController)
                    {
                        connectionsToRemove.Add(model.ConnectionControllers[i]);
                    }
                }

                for (var i = 0; i < connectionsToRemove.Count; i++)
                {
                    model.ConnectionModels.Remove(connectionsToRemove[i].model);
                    model.ConnectionDrawers.Remove(connectionsToRemove[i].view);
                    model.ConnectionControllers.Remove(connectionsToRemove[i]);
                }
            }

            model.NodesModels.Remove(node.model);
            model.NodesDrawers.Remove(node.view);
            model.NodesControllers.Remove(node);
        }

        private void OnDrag(Vector2 delta)
        {
            model.drag = delta;

            for (var i = 0; i < model.NodesControllers.Count; i++)
            {
                model.NodesControllers[i].Drag(delta);
            }

            OnDragEvent?.Invoke(delta);

            delta /= model.zoom;
            model.zoomCoordsOrigin += delta;
            
            GUI.changed = true;
        }
    }
}