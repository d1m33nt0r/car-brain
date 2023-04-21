namespace NeuralNet.Core
{
    public abstract class BaseNeuronController<TSelfModel> where TSelfModel : BaseNeuronModel
    {
        protected TSelfModel model;
    }
}