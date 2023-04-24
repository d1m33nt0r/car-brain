using System.Collections.Generic;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core
{
    public class InputNeuronController : BaseNeuronController<InputNeuronModel>
    {
        public override void Activate(List<BaseNeuronModel> neurons, float bias = 0)
        {
            
        }

        public InputNeuronController(InputNeuronModel model) : base(model)
        {
        }
    }
}