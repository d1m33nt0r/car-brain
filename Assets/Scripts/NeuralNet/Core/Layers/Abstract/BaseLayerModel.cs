using System;
using System.Collections.Generic;

namespace NeuralNet.Core.Layers
{
    [Serializable]
    public abstract class BaseLayerModel<TSelfModel> where TSelfModel : BaseNeuronModel
    {
        public List<TSelfModel> neuronModels;
    }
}