using System;
using System.Collections.Generic;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Layers
{
    [Serializable]
    public class Layer<TSelfModel> where TSelfModel : BaseNeuron
    {
        public float bias;
        public List<TSelfModel> neuronModels;

        public Layer(List<TSelfModel> neuronModels, float layerBias)
        {
            this.neuronModels = neuronModels;
            bias = layerBias;
        }
    }
}