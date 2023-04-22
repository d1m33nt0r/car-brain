using System;
using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Neurons.Abstract
{
    [Serializable]
    public abstract class BaseNeuronModel : BaseModel
    {
        public float data;

        public BaseNeuronModel(float data)
        {
            this.data = data;
        }
    }
}