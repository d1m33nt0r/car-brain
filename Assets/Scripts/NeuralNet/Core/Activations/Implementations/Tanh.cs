using System;

namespace NeuralNet.Core.Activations
{
    public class Tanh : BaseActivationController
    {
        public Tanh(ActivationModel model) : base(model)
        {
        }
        
        public override float Apply(float weightedSum)
        {
            return (float)Math.Tanh(weightedSum);
        }
    }
}