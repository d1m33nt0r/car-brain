using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core.Neurons.Output;

namespace NeuralNet.Core
{
    public class BrainController
    {
        public NeuralNetworkData Data => data;
        private NeuralNetworkData data;
        public Dictionary<int, Neuron> allNeurons { get; }

        public BrainController(string brainPath, bool randomize)
        {
            data = Serializer.ReadFromJson(brainPath);
            allNeurons = new Dictionary<int, Neuron>();
            foreach (var neuron in data.inputNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
            foreach (var neuron in data.hiddenNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
            foreach (var neuron in data.outputNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }

            if (randomize)
            {
                var hiddenNeurons = data.hiddenNeurons;
                foreach (var neuron in hiddenNeurons)
                {
                    neuron.bias += UnityEngine.Random.Range(-1f, 1f);
                    foreach (var weight in neuron.inputWeights)
                    {
                        weight.data += UnityEngine.Random.Range(-1f, 1f);
                    }
                }
            }
        }

        public BrainController(NeuralNetworkData data)
        {
            this.data = data;
            allNeurons = new Dictionary<int, Neuron>();
            foreach (var neuron in data.inputNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
            foreach (var neuron in data.hiddenNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
            foreach (var neuron in data.outputNeurons)
            {
                allNeurons.Add(neuron.id, neuron);
            }
        }

        public BrainController Copy()
        {
            var nnd = new NeuralNetworkData()
            {
                fitness = data.fitness,
                nextID = data.nextID
            };
            
            foreach (var neuron in data.inputNeurons)
            {
                nnd.inputNeurons.Add(neuron.Copy());
            }
            
            foreach (var neuron in data.hiddenNeurons)
            {
                nnd.hiddenNeurons.Add(neuron.Copy());
            }
            
            foreach (var neuron in data.outputNeurons)
            {
                nnd.outputNeurons.Add(neuron.Copy());
            }

            var bc = new BrainController(nnd);
            bc.Data.fitness = data.fitness;
         
            return bc;
        }

        public double[] FeedForward(float[] input)
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
        
        public double[] FeedForward(double[] input)
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
        
        public void BackPropagation(float[] input, float[] expected, float learningRate = 0.1f)
        {
            var output = FeedForward(input);
            
            for (var i = 0; i < output.Length; i++)
            {
                data.outputNeurons[i].delta = Math.Pow(output[i] - expected[i], 2); // to math pow, maybe
            }

            // Change to weights
            for (var i = 0; i < data.outputNeurons.Count; i++)
            {
                var currentNeuron = data.outputNeurons[i];
                for (var w = 0; w < data.outputNeurons[i].inputWeights.Count; w++)
                {
                    var currentWeight = currentNeuron.inputWeights[w];
                    
                    var deltaWeight = currentNeuron.delta * currentWeight.data * currentNeuron.Derivative() * learningRate;
                    currentWeight.data += deltaWeight;
                }
            }
            
            for (var n = data.hiddenNeurons.Count - 1; n >= 0; n--)
            {
                var neuron = data.hiddenNeurons[n];
                double diff = 0;
                for (var w = 0; w < neuron.outputWeights.Count; w++)
                {
                    var weight = neuron.outputWeights[w];
                    diff += Math.Pow(allNeurons[weight.outputNeuronID].data - allNeurons[weight.outputNeuronID].delta, 2);
                }
                diff /= neuron.outputWeights.Count;
                neuron.delta = diff;
            }
            
            // Change to weights
            for (var i = data.hiddenNeurons.Count - 1; i >= 0; i--)
            {
                var currentNeuron = data.hiddenNeurons[i];
                for (var w = 0; w < data.hiddenNeurons[i].inputWeights.Count; w++)
                {
                    var currentWeight = currentNeuron.inputWeights[w];
                    
                    var deltaWeight = currentNeuron.delta * currentWeight.data * currentNeuron.Derivative() * learningRate;
                    currentWeight.data += deltaWeight;
                }
            }
        }

        public void Save(string assetName)
        {
            Serializer.WriteToJson("Assets/" + assetName + ".json", data, false);
        }
    }
}