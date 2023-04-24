using System;
using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Activations
{
    [Serializable]
    public class ActivationModel : BaseModel
    {
        public ActivationType Type;
    }
}