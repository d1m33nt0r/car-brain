namespace NeuralNet.Core.Activations
{
    public class Relu : BaseActivationController
    {
        public Relu(ActivationModel model) : base(model)
        {
        }
        
        public override double Apply(double weightedSum)
        {
            return (0 >= weightedSum) ? 0 : weightedSum;
        }

        public override double Derivative(double x)
        {
            return default;
        }
    }
}