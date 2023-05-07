using System.Collections.Generic;

namespace NeuralNet.Core.Activations
{
    public class Activation
    {
        private static Activation instance;
        public static Activation Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Activation();
                }

                return instance;
            }
        }

        private Dictionary<ActivationType, BaseActivationController> activations;

        public Activation()
        {
            activations = new Dictionary<ActivationType, BaseActivationController>();
            var sigmoidModel = new ActivationModel{Type = ActivationType.Sigmoid};
            activations.Add(sigmoidModel.Type, new Sigmoid(sigmoidModel));
            var reluModel = new ActivationModel{Type = ActivationType.Relu};
            activations.Add(reluModel.Type, new Relu(reluModel));
            var leakyRelu = new ActivationModel{Type = ActivationType.Leakyrelu};
            activations.Add(leakyRelu.Type, new Leakyrelu(leakyRelu));
            var tanh = new ActivationModel{Type = ActivationType.Tanh};
            activations.Add(tanh.Type, new Tanh(tanh));
        }
        
        public float Apply(ActivationType activationType, float weightedSum)
        {
            return activations[activationType].Apply(weightedSum);
        }

        public float Derivative(ActivationType activationType, float neuronActiveValue)
        {
            return activations[activationType].Derivative(neuronActiveValue);
        }
    }
}