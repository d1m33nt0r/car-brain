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
    }
}