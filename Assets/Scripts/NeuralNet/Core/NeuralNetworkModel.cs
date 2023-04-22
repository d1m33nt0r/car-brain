using System;
using System.Collections.Generic;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Input;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core
{
    [Serializable]
    public class NeuralNetworkModel
    {
        public BaseLayerModel<InputNeuronModel> inputLayerModel;
        public List<BaseLayerModel<HiddenNeuronModel>> hiddenLayersModels;
        public BaseLayerModel<OutputNeuronModel> outputLayerModel;
    }
}