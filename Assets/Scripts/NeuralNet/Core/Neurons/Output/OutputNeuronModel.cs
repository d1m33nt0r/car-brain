using System;
using System.Collections.Generic;
using NeuralNet.Core.Activations;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class OutputNeuronModel : BaseNeuronModel
    {
        public BaseFunctionActivation FunctionActivation;
        public List<WeightModel> weights;
        
        public OutputNeuronModel(List<WeightModel> weights, BaseFunctionActivation functionActivation)
        {
            this.weights = weights;
            FunctionActivation = functionActivation;
        }
    }
}