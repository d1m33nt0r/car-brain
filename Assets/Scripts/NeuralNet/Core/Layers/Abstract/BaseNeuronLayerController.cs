namespace NeuralNet.Core.Layers
{
    public abstract class BaseNeuronLayerController<TSelfModel> where TSelfModel : BaseNeuronLayerModel<BaseNeuronModel>
    {
        public TSelfModel model;

        public BaseNeuronLayerController(TSelfModel model)
        {
            this.model = model;
        }
    }
}