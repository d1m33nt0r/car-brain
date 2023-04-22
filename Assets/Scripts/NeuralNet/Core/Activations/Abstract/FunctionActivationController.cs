using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Activations
{
    public abstract class FunctionActivationController : BaseController<FunctionActivationModel>
    {
        public abstract float Apply(float weightedSum, float bias = 0);

        protected FunctionActivationController(FunctionActivationModel model) : base(model)
        {
        }
    }
}