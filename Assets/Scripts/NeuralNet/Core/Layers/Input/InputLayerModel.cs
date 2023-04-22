using System.Collections.Generic;
using NeuralNet.Core.Neurons.Input;

namespace NeuralNet.Core.Layers
{
    public class InputLayerModel : BaseLayerModel<InputNeuronModel>
    {
        public InputLayerModel(IReadOnlyList<float> inputs)
        {
            for (var i = 0; i < inputs.Count; i++)
            {
                neuronModels[i].data = inputs[i];
            }
        }
    }
}