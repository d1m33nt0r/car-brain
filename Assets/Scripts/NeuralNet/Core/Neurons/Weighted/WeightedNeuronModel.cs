using System;
using System.Collections.Generic;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class WeightedNeuronModel : BaseNeuronModel
    {
        public ActivationType activationType;
        public List<WeightModel> weights;
        
        public WeightedNeuronModel(List<WeightModel> weights, ActivationType activationType, float data) : base(data)
        {
            this.weights = weights;
            this.activationType = activationType;
        }
    }
}