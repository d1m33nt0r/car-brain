using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Activations
{
    public abstract class BaseActivationController : BaseController<ActivationModel>
    {
        public abstract float Apply(float weightedSum, float bias = 0);

        protected BaseActivationController(ActivationModel model) : base(model)
        {
        }
    }
}