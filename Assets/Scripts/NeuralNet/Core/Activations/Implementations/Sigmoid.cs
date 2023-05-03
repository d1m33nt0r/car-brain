using System;

namespace NeuralNet.Core.Activations
{
    public class Sigmoid : BaseActivationController
    {
        public Sigmoid(ActivationModel model) : base(model)
        {
        }
        
        public override float Apply(float weightedSum)
        {
            var k = (float)Math.Exp(weightedSum);
            return k / (1.0f + k);
        }
    }
}