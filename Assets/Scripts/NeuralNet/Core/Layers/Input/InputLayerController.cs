using NeuralNet.Core.Neurons.Input;

namespace NeuralNet.Core.Layers.Input
{
    public class InputLayerController : BaseLayerController<InputLayerModel, InputNeuronModel>
    {
        public InputLayerController(InputLayerModel model) : base(model)
        {
            
        }
    }
}