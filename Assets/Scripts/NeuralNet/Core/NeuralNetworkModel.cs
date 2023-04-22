using System;
using System.Collections.Generic;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core
{
    [Serializable]
    public class NeuralNetworkModel
    {
        public LayerModel<InputNeuronModel> inputLayerModel;
        public List<LayerModel<WeightedInputNeuronModel<>>> hiddenLayersModels;
        public LayerModel<WeightedInputNeuronModel<>> outputLayerModel;
    }
}