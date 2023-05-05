using System.Collections.Generic;
using NeuralNet.Core.Activations;

namespace NeuralNet.Editor.Template
{
    public class NewAssetData
    {
        public int inputs;
        public int outputs;
        public int hiddens;
        public List<int> hiddenLayers;
        public ActivationType outputActivationType;
        public List<ActivationType> hiddenActivationTypes;
        public float neuronRandomRange;
        public float weightRandomRange;
    }
}