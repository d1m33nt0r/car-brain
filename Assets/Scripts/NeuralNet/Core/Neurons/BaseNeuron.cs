using System;

namespace NeuralNet.Core.Neurons.Abstract
{
    [Serializable]
    public class BaseNeuron
    {
        public float data;

        public BaseNeuron(float data)
        {
            this.data = data;
        }
    }
}