using System;

namespace NeuralNet.Core.Activations
{
    public class Sigmoid : BaseActivationController
    {
        public Sigmoid(ActivationModel model) : base(model)
        {
        }
        
        public override double Apply(double weightedSum)
        {
            if (weightedSum > 38.53f) return 1.0f;
            if (weightedSum < -38.53f) return 0.0f;
            
            var k = (float)Math.Exp(weightedSum);
            return k / (1.0f + k);
        }
        
        public override double Derivative(double x)
        {
            return x * (1 - x);
        }
    }
}