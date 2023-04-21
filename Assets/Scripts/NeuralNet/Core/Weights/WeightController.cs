namespace NeuralNet.Core
{
    public class WeightController
    {
        private WeightModel weightModel;
        
        public WeightController(WeightModel weightModel)
        {
            this.weightModel = weightModel;
        }

        public void ChangeWeight(float newWeight)
        {
            weightModel.data = newWeight;
        }
    }
}