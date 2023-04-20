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
            }
        }
        
        public void ProcessNodesEvents(Event e)
        {
            if (model.NodesControllers != null)
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
        }
 
        private void ProcessContextMenu(Vector2 mousePosition)
        {
            var genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition)); 
            genericMenu.ShowAsContext();
        }
 
        private void OnClickAddNode(Vector2 mousePosition)
        {
            var nodeTitle = $"{Constants.General.DEFAULT_NODE_TITLE}{Constants.General.NODE_SUFFIX}{model.NodesModels.Count}";
            var nodeModel = new BaseNodeModel(mousePosition, 
                Constants.General.DEFAULT_NODE_SIZE.width, 
                Constants.General.DEFAULT_NODE_SIZE.height, 
                nodeTitle, OnClickInPoint, OnClickOutPoint);
            var nodeDrawer = new BaseNodeDrawer();
            var nodeController = new BaseNodeController();
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
                if (model.SelectedOutPointController.model.BaseNodeModel != model.SelectedInPointController.model.BaseNodeModel)
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
                if (model.SelectedOutPointController.model.BaseNodeModel != model.SelectedInPointController.model.BaseNodeModel)
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
            /*if (connections == null)
            {
                connections = new List<Connection>();
            }*/
 
            
            
            model.ConnectionModels.Add(new BaseConnectionModel(model.SelectedInPointController, model.SelectedOutPointController));
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
    }
}