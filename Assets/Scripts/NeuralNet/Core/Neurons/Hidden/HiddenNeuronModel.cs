using System;
using System.Collections.Generic;

namespace NeuralNet.Core
{
    [Serializable]
    public class HiddenNeuronModel : BaseNeuronModel
    {
        public List<WeightModel> weights;
        
        public HiddenNeuronModel(List<WeightModel> weights)
        {
            this.weights = weights;
        }
    }
}