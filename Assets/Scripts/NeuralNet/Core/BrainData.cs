using System.Collections.Generic;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Neurons;
using NeuralNet.Core.Neurons.Output;
using UnityEngine;

namespace NeuralNet.Core
{
    public class BrainData : ScriptableObject
    {
        public string brainName;
        public float fitness;
        public List<Neuron> inputNeurons;
        public List<Neuron> hiddenNeurons;
        public List<Neuron> outputNeurons;
        public List<int> allNeurons;

        public int nextID;

        public BrainData()
        {
            hiddenNeurons = new List<Neuron>();
            inputNeurons = new List<Neuron>();
            outputNeurons = new List<Neuron>();
            allNeurons = new List<int>();
        }
        
        public void Init(int inputs, 
            int outputs, 
            int[] hiddenLayers, 
            ActivationType outputActivationType, 
            ActivationType[] hiddenActivationTypes, 
            float neuronRandomRange, 
            float weightRandomRange)
        {
            hiddenNeurons = new List<Neuron>();
            inputNeurons = new List<Neuron>();
            outputNeurons = new List<Neuron>();

            var horizontalSpaceLength = 200;
            var verticalSpaceLength = 100;
            var startNodesPosition = Vector2.zero;
            
            for (var i = 0; i < inputs; i++)
            {
                var newNeuron = new Neuron(nextID, 0, 0, new Vector2 ( startNodesPosition.x, startNodesPosition.y + i * verticalSpaceLength));
                newNeuron.neuronType = NeuronType.Input;
                newNeuron.onChangedNeuronType += OnChangedNeuronType;
                inputNeurons.Add(newNeuron);
                allNeurons.Add(newNeuron.id);
                nextID++;
            }

            var previousHiddenLayer = new List<int>();
            
            for (var i = 0; i < hiddenLayers.Length; i++)
            {
                var nextHiddenLayer = new List<int>();
                
                for (var j = 0; j < hiddenLayers[i]; j++)
                {
                    var newNeuron = new Neuron(nextID, -0.5f, 0.5f, new Vector2 ( startNodesPosition.x + (i + 1) * horizontalSpaceLength, startNodesPosition.y + j * verticalSpaceLength));
                    newNeuron.onChangedNeuronType += OnChangedNeuronType;
                    hiddenNeurons.Add(newNeuron);
                    allNeurons.Add(newNeuron.id);
                    nextID++;
                    if (i == 0)
                    {
                        for (var w = 0; w < inputNeurons.Count; w++)
                        {
                            var weight = new Weight(inputNeurons[w].id, newNeuron.id, -0.5f, 0.5f);
                            newNeuron.AddInputWeight(weight);
                            var neuron = inputNeurons.Find(n => n.id == weight.inputNeuronID);
                            neuron.AddOutputWeight(weight);
                        }
                    }
                    else
                    {
                        for (var w = 0; w < previousHiddenLayer.Count; w++)
                        {
                            var weight = new Weight(previousHiddenLayer[w], newNeuron.id, -0.5f, 0.5f);
                            newNeuron.AddInputWeight(weight);
                            var neuron = hiddenNeurons.Find(n => n.id == weight.inputNeuronID);
                            neuron.AddOutputWeight(weight);
                        }
                    }
                    nextHiddenLayer.Add(newNeuron.id);
                }

                previousHiddenLayer = nextHiddenLayer;

            }
            
            for (var i = 0; i < outputs; i++)
            {
                var newNeuron = new Neuron(nextID, -0.5f, 0.5f, new Vector2 ( startNodesPosition.x +
                                                                              (hiddenLayers.Length + 1) * horizontalSpaceLength, startNodesPosition.y + i * verticalSpaceLength));
                newNeuron.onChangedNeuronType += OnChangedNeuronType;
                outputNeurons.Add(newNeuron);
                allNeurons.Add(newNeuron.id);
                nextID++;
                for (var w = 0; w < previousHiddenLayer.Count; w++)
                {
                    var weight = new Weight(previousHiddenLayer[w], newNeuron.id, -0.5f, 0.5f);
                    newNeuron.AddInputWeight(weight);
                    var neuron = hiddenNeurons.Find(n => n.id == weight.inputNeuronID);
                    neuron.AddOutputWeight(weight);
                }
                newNeuron.neuronType = NeuronType.Output;
            }
        }
        
        public Neuron AddNeuron(Vector2 position)
        {
            var newNeuron = new Neuron(nextID, -0.5f, 0.5f, position);
            newNeuron.onChangedNeuronType += OnChangedNeuronType;
            hiddenNeurons.Add(newNeuron);
            allNeurons.Add(newNeuron.id);
            nextID++;
            return newNeuron;
        }

        public Weight AddWeight(Neuron fromNeuron, Neuron toNeuron)
        {
            var newWeight = new Weight(fromNeuron.id, toNeuron.id, -0.5f, 0.5f);
            newWeight.AttachNeurons(fromNeuron, toNeuron);
            toNeuron.AddInputWeight(newWeight);
            fromNeuron.AddOutputWeight(newWeight);
            return newWeight;
        }

        public void RemoveNeuron(Neuron neuron)
        {
            if (hiddenNeurons.Contains(neuron)) hiddenNeurons.Remove(neuron);
            if (inputNeurons.Contains(neuron)) inputNeurons.Remove(neuron);
            if (outputNeurons.Contains(neuron)) outputNeurons.Remove(neuron);
            if (allNeurons.Contains(neuron.id)) allNeurons.Remove(neuron.id);
        }

        public void RemoveWeight(Weight weight)
        {
            
            weight.outputNeuron.RemoveInputWeight(weight);
        }

        private void OnChangedNeuronType(Neuron neuron, NeuronType prevNeuronType, NeuronType newNeuronType)
        {
            if (prevNeuronType == NeuronType.Hidden && newNeuronType == NeuronType.Input)
            {
                neuron.bias = 0;
                inputNeurons.Add(neuron);
                hiddenNeurons.Remove(neuron);
            }

            if (prevNeuronType == NeuronType.Hidden && newNeuronType == NeuronType.Output)
            {
                neuron.bias = 0;
                outputNeurons.Add(neuron);
                hiddenNeurons.Remove(neuron);
            }

            if (prevNeuronType == NeuronType.Input)
            {
                inputNeurons.Remove(neuron);
                
                switch (newNeuronType)
                {
                    case NeuronType.Output:
                        neuron.bias = 0;
                        outputNeurons.Add(neuron);
                        break;
                    case NeuronType.Hidden:
                        neuron.bias = UnityEngine.Random.Range(-0.5f, 0.5f);
                        hiddenNeurons.Add(neuron);
                        break;
                }
            }

            if (prevNeuronType == NeuronType.Output)
            {
                outputNeurons.Remove(neuron);
                switch (newNeuronType)
                {
                    case NeuronType.Input:
                        neuron.bias = 0;
                        inputNeurons.Add(neuron);
                        break;
                    case NeuronType.Hidden:
                        neuron.bias = UnityEngine.Random.Range(-0.5f, 0.5f);
                        hiddenNeurons.Add(neuron);
                        break;
                }
            }
        }
    }
}