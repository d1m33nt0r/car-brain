using NeuralNet.Core.Activations;
using UnityEngine;

namespace NeuralNet.Core
{
    public class NeuralNetworkController
    {
        private NeuralNetworkData data;
        
        public NeuralNetworkController(NeuralNetworkData neuralNetworkData)
        {
            data = neuralNetworkData;
            
        }

        public float[] FeedForward(float[] inputs)
        {
            if (inputs.Length != this.data.inputLayer.neuronModels.Count) 
                Debug.LogWarning("The number of input layer neurons does not match the input data");
            
            for (var i = 0; i < inputs.Length; i++)
            {
                if (i < this.data.inputLayer.neuronModels.Count)
                    this.data.inputLayer.neuronModels[i].data = inputs[i];
            }

            for (var i = 0; i < this.data.hiddenLayersModels.Count; i++)
            {
                var currentLayer = this.data.hiddenLayersModels[i];
                for (var j = 0; j < currentLayer.neuronModels.Count; j++)
                {
                    var weightedSum = 0f;
                    var currentNeuronWeights = currentLayer.neuronModels[j].weights;
                    for (var n = 0; n < currentNeuronWeights.Count; n++)
                    {
                        if (i == 0)
                        {
                            var weight = this.data.inputLayer.neuronModels[currentNeuronWeights[n].previousNeuronIndex].data *
                                currentNeuronWeights[n].data;
                            weightedSum += weight;
                        }

                        if (i > 0)
                        {
                            var weight = this.data.hiddenLayersModels[i - 1].neuronModels[currentNeuronWeights[n].previousNeuronIndex].data *
                                         currentNeuronWeights[n].data;
                            weightedSum += weight;
                        }
                    }

                    weightedSum += currentLayer.bias;
                    var activatedValue = Activation.Instance.Apply(currentLayer.neuronModels[j].activationType, weightedSum);
                    currentLayer.neuronModels[j].data = activatedValue;
                }
            }
            
            for (var i = 0; i < this.data.outputLayer.neuronModels.Count; i++)
            {
                var weightedSum = 0f;
                var currentNeuronWeights = this.data.outputLayer.neuronModels[i].weights;
                for (var j = 0; j < currentNeuronWeights.Count; j++)
                {
                    var weight = this.data.hiddenLayersModels[i - 1].neuronModels[currentNeuronWeights[j].previousNeuronIndex].data *
                                 currentNeuronWeights[j].data;
                    weightedSum += weight;
                }
                weightedSum += this.data.outputLayer.bias;
                var activatedValue = Activation.Instance.Apply(this.data.outputLayer.neuronModels[i].activationType, weightedSum);
                this.data.outputLayer.neuronModels[i].data = activatedValue;
            }

            var data = new float [this.data.outputLayer.neuronModels.Count];

            for (var i = 0; i < this.data.outputLayer.neuronModels.Count; i++)
            {
                data[i] = this.data.outputLayer.neuronModels[i].data;
            }

            return data;
        }
    }
}