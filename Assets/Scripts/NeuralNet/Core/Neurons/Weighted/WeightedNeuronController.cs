using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Neurons.Output
{
    public class WeightedNeuronController : BaseNeuronController<WeightedNeuronModel>
    {
        public WeightedNeuronController(WeightedNeuronModel model) : base(model)
        {
          
        }
        
        public override void Activate(List<BaseNeuronModel> neurons, float bias)
        {
            var weights = model.weights;
            var weightedSum = weights.Sum(weightModel => neurons[weightModel.neuronIndex].data * weightModel.data);
            var activatedValue = Activation.Instance.Apply(model.activationType, weightedSum + bias);
            model.data = activatedValue;
        }
    }
}