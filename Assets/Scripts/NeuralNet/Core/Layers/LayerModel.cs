using System;
using System.Collections.Generic;
using NeuralNet.Core.Neurons.Abstract;

namespace NeuralNet.Core.Layers
{
    [Serializable]
    public class LayerModel<TSelfModel> where TSelfModel : BaseNeuronModel
    {
        public List<TSelfModel> neuronModels;

        public LayerModel(List<TSelfModel> neuronModels)
        {
            this.neuronModels = neuronModels;
        }
    }
}