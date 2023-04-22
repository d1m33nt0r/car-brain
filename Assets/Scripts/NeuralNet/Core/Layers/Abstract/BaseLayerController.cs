namespace NeuralNet.Core.Layers
{
    public abstract class BaseLayerController<TSelfModel, TNeuronModel> where TSelfModel : BaseLayerModel<TNeuronModel> where TNeuronModel: BaseNeuronModel
    {
        public TSelfModel model;

        public BaseLayerController(TSelfModel model)
        {
            this.model = model;
        }
    }
}