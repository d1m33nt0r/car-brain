using System.Collections.Generic;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Abstract;
using NeuralNet.Core.Neurons.Output;
using UnityEngine;

namespace NeuralNet.Core
{
    [CreateAssetMenu(fileName = "BrainAsset", menuName = "NeuralNet/Brain Asset")]
    public class NeuralNetworkData : ScriptableObject
    {
        
        public Layer<BaseNeuron> inputLayer;
        public List<Layer<WeightedNeuron>> hiddenLayersModels;
        public Layer<WeightedNeuron> outputLayer;

        public NeuralNetworkData(Layer<BaseNeuron> inputLayer, 
            List<Layer<WeightedNeuron>> hiddenLayersModels, 
            Layer<WeightedNeuron> outputLayer)
        {
            this.hiddenLayersModels = hiddenLayersModels;
            this.inputLayer = inputLayer;
            this.outputLayer = outputLayer;
        }
    }
}