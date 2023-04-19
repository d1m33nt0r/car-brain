using System.Collections.Generic;
using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Nodes.BaseNode;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class NeuralNetworkModel : BaseModel
    {
        public List<BaseNodeDrawer> NodesDrawers;
        public List<BaseNodeModel> NodesModels;
        public List<BaseNodeController> NodesControllers;

        public NeuralNetworkModel()
        {
            NodesDrawers = new List<BaseNodeDrawer>();
            NodesModels = new List<BaseNodeModel>();
            NodesControllers = new List<BaseNodeController>();
        }

        public NeuralNetworkModel(List<BaseNodeDrawer> nodesDrawers, List<BaseNodeModel> nodesModels, List<BaseNodeController> nodeControllers)
        {
            NodesDrawers = nodesDrawers;
            NodesModels = nodesModels;
            NodesControllers = nodeControllers;
        }
    }
}