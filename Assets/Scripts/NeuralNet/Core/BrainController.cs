using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core
{
    public class BrainController
    {
        private NeuralNetworkData data;
        public Dictionary<int, Neuron> allNeurons { get; }

        public BrainController(string brainPath, bool enableMutation, float fitnessThreshold)
        {
            data = Serializer.ReadFromJson(brainPath);
            allNeurons = new Dictionary<int, Neuron>();
            foreach (var neuron in data.inputNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
        }

        public BrainController(NeuralNetworkData data, bool enableMutation, float fitnessThreshold)
        {
            this.data = data;
            allNeurons = new Dictionary<int, Neuron>();
            foreach (var neuron in data.inputNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
        }

        public float[] FeedForward(float[] input)
        {
            if (input.Length > data.inputNeurons.Count)
                throw new Exception(
                    "Discrepancy between the number of signals given and the number of neural network inputs");
            
            for (var i = 0; i < data.inputNeurons.Count; i++)
            {
                if (i < input.Length)
                {
                    data.inputNeurons[i].data = input[i];
                }
                else
                {
                    data.inputNeurons[i].data = default;
                    throw new Exception(
                        "The input neuron is assigned a default value because the number of signals given is less than the number of inputs for the neural network");
                }
            }

            for (var i = 0; i < data.hiddenNeurons.Count; i++)
            {
                data.hiddenNeurons[i].Activate(this);
            }
            
            for (var i = 0; i < data.outputNeurons.Count; i++)
            {
                data.outputNeurons[i].Activate(this);
            }

            return data.outputNeurons.Select(p => p.data).ToArray();
        }

        public void Mutate(int chance, int chanceThreshold, float mutationMinVal, float mutationMaxVal, bool mutateOutputBiases)
        {
            for (var n = 0; n < data.hiddenNeurons.Count; n++)
            {
                // mutate hidden biases
                var randValue2 = UnityEngine.Random.Range(0, chanceThreshold);
                if (randValue2 <= chance) data.hiddenNeurons[n].bias += UnityEngine.Random.Range(mutationMinVal, mutationMaxVal);
                // mutate hidden weights
                for (var w = 0; w < data.hiddenNeurons[n].inputWeights.Count; w++)
                {
                    var randValue = UnityEngine.Random.Range(0, chanceThreshold);
                    if (randValue <= chance)
                    {
                        data.hiddenNeurons[n].inputWeights[w].data += UnityEngine.Random.Range(mutationMinVal, mutationMaxVal);
                    }
                }
            }
            
            for (var n = 0; n < data.outputNeurons.Count; n++)
            {
                if (mutateOutputBiases)
                    // mutate output biases
                {
                    var randValue = UnityEngine.Random.Range(0, chanceThreshold);
                    if (randValue <= chance) data.hiddenNeurons[n].bias += UnityEngine.Random.Range(mutationMinVal, mutationMaxVal);
                }
                // mutate output weights
                for (var w = 0; w < data.outputNeurons[n].inputWeights.Count; w++)
                {
                    var randValue = UnityEngine.Random.Range(0, chanceThreshold);
                    if (randValue <= chance)
                    {
                        data.outputNeurons[n].inputWeights[w].data += UnityEngine.Random.Range(mutationMinVal, mutationMaxVal);
                    }
                }
            }
        }
    }
}