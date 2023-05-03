using System;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core
{
    [Serializable]
    public class Weight
    {
        public int inputNeuronID;
        public int outputNeuronID;
        public float data;

        public Neuron inputNeuron { get; private set; }
        public Neuron outputNeuron { get; private set; }
        
        public Weight(int inputNeuronID, int outputNeuronID, float minRange, float maxRange)
        {
            this.inputNeuronID = inputNeuronID;
            this.outputNeuronID = outputNeuronID;
            data = UnityEngine.Random.Range(minRange, maxRange);
        }

        public void AttachNeurons(Neuron inputNeuron, Neuron outputNeuron)
        {
            this.inputNeuron = inputNeuron;
            this.outputNeuron = outputNeuron;
        }

        public Weight()
        {
            
        }

        public Weight Copy()
        {
            return new Weight()
            {
                inputNeuronID = inputNeuronID,
                data = data,
                outputNeuronID = outputNeuronID
            };
        }
    }
}