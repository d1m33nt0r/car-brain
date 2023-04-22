using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Neurons.Abstract
{
    public abstract class BaseNeuronController<TSelfModel> : BaseController<TSelfModel> where TSelfModel : BaseNeuronModel
    {
        protected BaseNeuronController(TSelfModel model) : base(model)
        {
        }
    }
}