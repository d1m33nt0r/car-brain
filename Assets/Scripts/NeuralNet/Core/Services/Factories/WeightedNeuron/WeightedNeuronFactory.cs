using NeuralNet.Core.Abstract;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Common;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Abstract;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core.Services.Factories.WeightedNeuron
{
    public class WeightedNeuronFactory<TPreviousLayerController, TPreviousNeuronModel> : IFactory<WeightedInputNeuronController<Sigmoid, TPreviousLayerController, TPreviousNeuronModel, SigmoidFactory, EmptyArgs>, WeightedInputNeuronModel<Sigmoid>, EmptyArgs> where TPreviousLayerController : LayerController<LayerModel<TPreviousNeuronModel>, TPreviousNeuronModel> where TPreviousNeuronModel : BaseNeuronModel
    {
        public WeightedInputNeuronController<Sigmoid, TPreviousLayerController, TPreviousNeuronModel, SigmoidFactory, EmptyArgs> Create(EmptyArgs args = null)
        {
            
        }
    }
}