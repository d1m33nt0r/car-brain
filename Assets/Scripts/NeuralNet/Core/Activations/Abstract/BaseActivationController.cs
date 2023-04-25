namespace NeuralNet.Core.Activations
{
    public abstract class BaseActivationController
    {
        private ActivationModel model;
        public abstract float Apply(float weightedSum);

        protected BaseActivationController(ActivationModel model)
        {
            this.model = model;
        }
    }
}