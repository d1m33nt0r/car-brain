using System;

namespace NeuralNet.Core
{
    [Serializable]
    public class Weight
    {
        public int previousNeuronIndex;
        public float data;
    }
}