using System;

namespace NeuralNet.Core.Activations
{
    [Serializable]
    public abstract class BaseFunctionActivation
    {
        public ActivationType Type;
        public abstract float Activation(float weightedSum, float bias);
    }
}