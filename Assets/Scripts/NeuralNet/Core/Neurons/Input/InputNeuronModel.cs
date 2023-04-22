using System;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core
{
    [Serializable]
    public class InputNeuronModel : BaseNeuronModel
    {
        public InputNeuronModel(float data) : base(data)
        {
        }
    }
}