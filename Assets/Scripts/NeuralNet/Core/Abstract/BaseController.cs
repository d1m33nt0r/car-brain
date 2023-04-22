namespace NeuralNet.Core.Abstract
{
    public abstract class BaseController<TSelfModel> where TSelfModel : BaseModel
    {
        public TSelfModel model;

        public BaseController(TSelfModel model)
        {
            this.model = model;
        }
    }
}