using NeuralNet.Core.Abstract;

namespace NeuralNet.Core
{
    public class WeightController : BaseController<WeightModel>
    {
        private WeightModel weightModel;
        
        public WeightController(WeightModel weightModel) : base(weightModel)
        {
            this.weightModel = weightModel;
        }

        public void ChangeWeight(float newWeight)
        {
            weightModel.data = newWeight;
        }
    }
}