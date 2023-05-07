namespace NeuralNet.Core.Activations
{
    public abstract class BaseActivationController
    {
        private ActivationModel model;
        public abstract float Apply(float weightedSum);
        
        public abstract float Derivative(float x);

        protected BaseActivationController(ActivationModel model)
        {
            this.model = model;
        }
    }
}