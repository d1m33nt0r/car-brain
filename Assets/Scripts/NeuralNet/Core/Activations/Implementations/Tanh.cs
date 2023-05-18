using System;

namespace NeuralNet.Core.Activations
{
    public class Tanh : BaseActivationController
    {
        public Tanh(ActivationModel model) : base(model)
        {
        }
        
        public override double Apply(double weightedSum)
        {
            return (float)Math.Tanh(weightedSum);
        }

        public override double Derivative(double x)
        {
            return default;
        }
    }
}