using System;
using System.Collections.Generic;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class WeightedInputNeuronModel<TActivation> : BaseNeuronModel where TActivation : FunctionActivationController
    {
        public TActivation functionActivation;
        public List<WeightModel> weights;
        
        public WeightedInputNeuronModel(List<WeightModel> weights, TActivation functionActivation, float data) : base(data)
        {
            this.weights = weights;
            this.functionActivation = functionActivation;
        }
    }
}