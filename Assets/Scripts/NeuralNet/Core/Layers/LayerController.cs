using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Layers
{
    public class LayerController<TSelfModel, TNeuronModel> where TSelfModel : LayerModel<TNeuronModel> where TNeuronModel : BaseNeuronModel
    {
        public TSelfModel model;

        public LayerController(TSelfModel model)
        {
            this.model = model;
        }
    }
}