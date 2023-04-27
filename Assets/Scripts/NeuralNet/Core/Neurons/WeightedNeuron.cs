using System;
using System.Collections.Generic;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons.Abstract;
using UnityEngine;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class WeightedNeuron : BaseNeuron
    {
        public ActivationType activationType;
        public List<Weight> weights;
        
        public WeightedNeuron(List<Weight> weights, ActivationType activationType, float data, Vector2 position) : base(data, position)
        {
            this.weights = weights;
            this.activationType = activationType;
        }
    }
}