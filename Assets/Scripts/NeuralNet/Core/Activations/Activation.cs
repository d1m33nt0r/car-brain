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
        }
        
        public float Apply(ActivationType activationType, float weightedSum)
        {
            return activations[activationType].Apply(weightedSum);
        }
    }
}