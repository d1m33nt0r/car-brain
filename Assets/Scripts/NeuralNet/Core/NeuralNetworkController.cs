using System.IO;
using NeuralNet.Core.Activations;
using Newtonsoft.Json;
using UnityEngine;

namespace NeuralNet.Core
{
    public class NeuralNetworkController
    {
        private NeuralNetworkModel model;
        
        public NeuralNetworkController(NeuralNetworkModel neuralNetworkModel)
        {
            model = neuralNetworkModel;
        }

        public float[] FeedForward(float[] inputs)
        {
            if (inputs.Length != model.inputLayerModel.neuronModels.Count) 
                Debug.LogWarning("The number of input layer neurons does not match the input data");
            
            for (var i = 0; i < inputs.Length; i++)
            {
                if (i < model.inputLayerModel.neuronModels.Count)
                    model.inputLayerModel.neuronModels[i].data = inputs[i];
            }

            for (var i = 0; i < model.hiddenLayersModels.Count; i++)
            {
                var currentLayer = model.hiddenLayersModels[i];
                for (var j = 0; j < currentLayer.neuronModels.Count; j++)
                {
                    var weightedSum = 0f;
                    var currentNeuronWeights = currentLayer.neuronModels[j].weights;
                    for (var n = 0; n < currentNeuronWeights.Count; n++)
                    {
                        if (i == 0)
                        {
                            var weight = model.inputLayerModel.neuronModels[currentNeuronWeights[n].neuronIndex].data *
                                currentNeuronWeights[n].data;
                            weightedSum += weight;
                        }

                        if (i > 0)
                        {
                            var weight = model.hiddenLayersModels[i - 1].neuronModels[currentNeuronWeights[n].neuronIndex].data *
                                         currentNeuronWeights[n].data;
                            weightedSum += weight;
                        }
                    }

                    weightedSum += currentLayer.bias;
                    var activatedValue = Activation.Instance.Apply(currentLayer.neuronModels[j].activationType, weightedSum);
                    currentLayer.neuronModels[j].data = activatedValue;
                }
            }
            
            for (var i = 0; i < model.outputLayerModel.neuronModels.Count; i++)
            {
                var weightedSum = 0f;
                var currentNeuronWeights = model.outputLayerModel.neuronModels[i].weights;
                for (var j = 0; j < currentNeuronWeights.Count; j++)
                {
                    var weight = model.hiddenLayersModels[i - 1].neuronModels[currentNeuronWeights[j].neuronIndex].data *
                                 currentNeuronWeights[j].data;
                    weightedSum += weight;
                }
                weightedSum += model.outputLayerModel.bias;
                var activatedValue = Activation.Instance.Apply(model.outputLayerModel.neuronModels[i].activationType, weightedSum);
                model.outputLayerModel.neuronModels[i].data = activatedValue;
            }

            var data = new float [model.outputLayerModel.neuronModels.Count];

            for (var i = 0; i < model.outputLayerModel.neuronModels.Count; i++)
            {
                data[i] = model.outputLayerModel.neuronModels[i].data;
            }

            return data;
        }
    }
}