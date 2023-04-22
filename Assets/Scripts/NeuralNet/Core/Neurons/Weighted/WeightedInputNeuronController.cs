using System.Linq;
using NeuralNet.Core.Abstract;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Neurons.Output
{
    public class WeightedInputNeuronController<TSelfActivation, TPreviousLayer, TPreviousNeuronModel, TActivationFactory, TActivationFactoryArgs> : BaseNeuronController<WeightedInputNeuronModel<TSelfActivation>> 
        where TSelfActivation : FunctionActivationController 
        where TPreviousLayer : LayerController<LayerModel<TPreviousNeuronModel>, TPreviousNeuronModel> 
        where TPreviousNeuronModel : BaseNeuronModel 
        where TActivationFactory : IFactory<FunctionActivationController, FunctionActivationModel, TActivationFactoryArgs> 
        where TActivationFactoryArgs : BaseArgs
    {
        private TPreviousLayer previousLayer;
        private TActivationFactory activationFactory;
        private TActivationFactoryArgs activationFactoryArgs;
        
        public WeightedInputNeuronController(WeightedInputNeuronModel<TSelfActivation> model, TPreviousLayer previousLayer, TActivationFactory activationFactory, TActivationFactoryArgs activationFactoryArgs) : base(model)
        {
            this.previousLayer = previousLayer;
            this.activationFactory = activationFactory;
            this.activationFactoryArgs = activationFactoryArgs;
        }
        
        public void Activate()
        {
            var neurons = previousLayer.model.neuronModels;
            var activation = activationFactory.Create(activationFactoryArgs);
            var weights = model.weights;
            var weightedSum = weights.Sum(weightModel => neurons[weightModel.neuronIndex].data * weightModel.data);
            var activatedValue = activation.Apply(weightedSum);
            model.data = activatedValue;
        }
    }
}