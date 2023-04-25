using System;
using System.Collections.Generic;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class WeightedNeuron : BaseNeuron
    {
        public ActivationType activationType;
        public List<Weight> weights;
        
        public WeightedNeuron(List<Weight> weights, ActivationType activationType, float data) : base(data)
        {
            this.weights = weights;
            this.activationType = activationType;
        }
    }
}