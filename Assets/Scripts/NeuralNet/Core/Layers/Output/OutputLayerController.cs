using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core.Layers.Output
{
    public class OutputLayerController : BaseLayerController<OutputLayerModel, OutputNeuronModel>
    {
        public OutputLayerController(OutputLayerModel model) : base(model)
        {
            
        }
    }
}