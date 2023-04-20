using System.Collections.Generic;
using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Connections;
using NeuralNet.Editor.Nodes.BaseNode;
using UnityEngine;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class GraphModel : BaseModel
    {
        public BaseConnectionPointController SelectedInPointController;
        public BaseConnectionPointController SelectedOutPointController;
        
        public List<BaseNodeDrawer> NodesDrawers;
        public List<BaseNodeModel> NodesModels;
        public List<BaseNodeController> NodesControllers;

        public List<BaseConnectionDrawer> ConnectionDrawers;
        public List<BaseConnectionModel> ConnectionModels;
        public List<BaseConnectionController> ConnectionControllers;
        
        public Vector2 drag;
        public float kZoomMin = 0.1f;
        public float kZoomMax = 10.0f;
        
        public float zoom = 1.0f;
        public Vector2 zoomCoordsOrigin = Vector2.zero;
        
        public GraphModel()
        {
            NodesDrawers = new List<BaseNodeDrawer>();
            NodesModels = new List<BaseNodeModel>();
            NodesControllers = new List<BaseNodeController>();
            
            ConnectionDrawers = new List<BaseConnectionDrawer>();
            ConnectionControllers = new List<BaseConnectionController>();
            ConnectionModels = new List<BaseConnectionModel>();
        }

        public GraphModel(
            List<BaseNodeDrawer> nodesDrawers, 
            List<BaseNodeModel> nodesModels, 
            List<BaseNodeController> nodeControllers,
            
            List<BaseConnectionDrawer> connectionDrawers,
            List<BaseConnectionModel> connectionModels,
            List<BaseConnectionController> connectionControllers)
        {
            NodesDrawers = nodesDrawers;
            NodesModels = nodesModels;
            NodesControllers = nodeControllers;
            
            ConnectionControllers = connectionControllers;
            ConnectionDrawers = connectionDrawers;
            ConnectionModels = connectionModels;
        }
    }
}