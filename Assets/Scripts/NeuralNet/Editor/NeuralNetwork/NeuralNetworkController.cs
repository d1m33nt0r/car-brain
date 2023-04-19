using System;
using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Nodes.BaseNode;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class NeuralNetworkController : BaseController<NeuralNetworkDrawer, NeuralNetworkModel>
    {
        public event Action OnModelChanged;
        
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
            var nodeModel = new BaseNodeModel(mousePosition, Constants.General.DEFAULT_NODE_SIZE.width, Constants.General.DEFAULT_NODE_SIZE.height, nodeTitle);
            var nodeDrawer = new BaseNodeDrawer();
            var nodeController = new BaseNodeController();
            nodeController.AttachModel(nodeModel);
            nodeController.AttachDrawer(nodeDrawer);
            model.NodesModels.Add(nodeModel);
            model.NodesDrawers.Add(nodeDrawer);
            model.NodesControllers.Add(nodeController);
            OnModelChanged?.Invoke();
        }
    }
}