using System;
using System.Collections.Generic;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class OutputNeuronModel : BaseNeuronModel
    {
        public List<WeightModel> weights;
        
        public OutputNeuronModel(List<WeightModel> weights)
        {
            this.weights = weights;
        }
    }
}