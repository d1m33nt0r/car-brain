using System;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core
{
    [Serializable]
    public class Weight
    {
        public Neuron inputNeuron;
        public Neuron outputNeuron;
        public float data;
    }
}