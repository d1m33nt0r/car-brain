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

        public Weight(Neuron inputNeuron, Neuron outputNeuron, float minRange, float maxRange)
        {
            this.inputNeuron = inputNeuron;
            this.outputNeuron = outputNeuron;
            data = UnityEngine.Random.Range(minRange, maxRange);
        }
    }
}