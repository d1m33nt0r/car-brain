using System;

namespace NeuralNet.Core.Activations
{
    [Serializable]
    public class Sigmoid : FunctionActivationController
    {
        public Sigmoid(FunctionActivationModel model) : base(model)
        {
        }
        
        public override float Apply(float weightedSum, float bias = 0)
        {
            var k = (float)Math.Exp(weightedSum - bias);
            return k / (1.0f + k);
        }
    }
}