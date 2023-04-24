using System.Collections.Generic;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Layers
{
    public class LayerController<TSelfModel, TNeuronModel, TNeuronController> 
        where TSelfModel : LayerModel<TNeuronModel> 
        where TNeuronModel : BaseNeuronModel 
        where TNeuronController : BaseNeuronController<TNeuronModel>
    {
        public TSelfModel model;
        public List<TNeuronController> neuronsControllers;

        public LayerController(TSelfModel model, List<TNeuronController> neuronsControllers)
        {
            this.model = model;
            this.neuronsControllers = neuronsControllers;
        }
    }
}