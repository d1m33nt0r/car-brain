using System;

namespace NeuralNet.Core.Activations
{
    public class Sigmoid : BaseFunctionActivation
    {
        public ActivationType Type = ActivationType.Sigmoid;

        public override float Activation(float weightedSum, float bias = 0)
        {
            var k = (float)Math.Exp(weightedSum - bias);
            return k / (1.0f + k);
        }
    }
}