namespace NeuralNet.Core.Activations
{
    public class Leakyrelu : BaseActivationController
    {
        public Leakyrelu(ActivationModel model) : base(model)
        {
        }
        
        public override double Apply(double weightedSum)
        {
            return (0 >= weightedSum) ? 0.01f * weightedSum : weightedSum;
        }

        public override double Derivative(double x)
        {
            return default;
        }
    }
}