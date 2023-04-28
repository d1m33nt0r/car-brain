using System;

namespace NeuralNet.Core
{
    [Serializable]
    public class Weight
    {
        public int inputNeuronID;
        public int outputNeuronID;
        public float data;
    }
}