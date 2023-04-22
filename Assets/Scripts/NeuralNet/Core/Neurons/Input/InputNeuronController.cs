using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core
{
    public class InputNeuronController<TSelfModel> : BaseNeuronController<TSelfModel> where TSelfModel : InputNeuronModel
    {
        public void SetInput(float input)
        {
            
        }

        public InputNeuronController(TSelfModel model) : base(model)
        {
        }
    }
}