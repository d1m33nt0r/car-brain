using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Output;
using UnityEngine;

namespace NeuralNet.Core
{
    public class NeuralNetworkController
    {
        private NeuralNetworkModel model;
        
        private LayerController<LayerModel<InputNeuronModel>, InputNeuronModel, InputNeuronController> inputLayer;
        private List<LayerController<LayerModel<WeightedNeuronModel>, WeightedNeuronModel, WeightedNeuronController>> hiddenLayers;
        private LayerController<LayerModel<WeightedNeuronModel>, WeightedNeuronModel, WeightedNeuronController> outputLayer;
        
        public NeuralNetworkController(NeuralNetworkModel neuralNetworkModel)
        {
            model = neuralNetworkModel;
            
            var inputNeuronsControllers = model.inputLayerModel.neuronModels.Select(inputNeuronModel => new InputNeuronController(inputNeuronModel)).ToList();
            inputLayer = new LayerController<LayerModel<InputNeuronModel>, InputNeuronModel, InputNeuronController>(model.inputLayerModel, inputNeuronsControllers);
            
            hiddenLayers = new List<LayerController<LayerModel<WeightedNeuronModel>, WeightedNeuronModel>>();
            foreach (var hiddenLayerModel in model.hiddenLayersModels)
            {
                var hiddenLayer =
                    new LayerController<LayerModel<WeightedNeuronModel>, WeightedNeuronModel>(hiddenLayerModel);
                hiddenLayers.Add(hiddenLayer);
            }

            outputLayer =
                new LayerController<LayerModel<WeightedNeuronModel>, WeightedNeuronModel>(model.outputLayerModel);
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
                    }

                    weightedSum += currentLayer.bias;
                    currentLayer.neuronModels[j].data = 
                }
            }
        }

        public void Mutate()
        {
            
        }
        
        public float[] FeedFsorward(float[] inputs)//feed forward, inputs >==> outputs.
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < layers[i]; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < layers[i - 1]; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = activate(value + biases[i-1][j], layer);
                }
            }
            return neurons[layers.Length-1];
        }
    }
}