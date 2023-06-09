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
            if (x > 0) return 1;
            if (x == 0) return 0.5;
            return 0;
        }
    }
}