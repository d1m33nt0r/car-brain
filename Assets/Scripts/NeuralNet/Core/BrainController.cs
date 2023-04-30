using System;
using System.Linq;

namespace NeuralNet.Core
{
    public class BrainController
    {
        private NeuralNetworkData data;

        public BrainController(string brainPath, bool enableMutation, float fitnessThreshold)
        {
            data = Serializer.ReadFromJson(brainPath);
        }

        public BrainController(NeuralNetworkData data, bool enableMutation, float fitnessThreshold)
        {
            this.data = data;
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
                data.hiddenNeurons[i].Activate();
            }
            
            for (var i = 0; i < data.outputNeurons.Count; i++)
            {
                data.outputNeurons[i].Activate();
            }

            return data.outputNeurons.Select(p => p.data).ToArray();
        }
    }
}