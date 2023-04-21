using System;
using System.Collections.Generic;

namespace NeuralNet.Core.Layers
{
    [Serializable]
    public abstract class BaseNeuronLayerModel<TSelfModel> where TSelfModel : BaseNeuronModel
    {
        public List<TSelfModel> neuronModels;
    }
}