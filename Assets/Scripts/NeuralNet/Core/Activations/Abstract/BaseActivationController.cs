namespace NeuralNet.Core.Activations
{
    public abstract class BaseActivationController
    {
        private ActivationModel model;
        public abstract double Apply(double weightedSum);
        
        public abstract double Derivative(double x);

        protected BaseActivationController(ActivationModel model)
        {
            this.model = model;
        }
    }
}