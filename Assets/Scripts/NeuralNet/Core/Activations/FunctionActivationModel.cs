using System;
using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Activations
{
    [Serializable]
    public class FunctionActivationModel : BaseModel
    {
        public ActivationType Type;
    }
}