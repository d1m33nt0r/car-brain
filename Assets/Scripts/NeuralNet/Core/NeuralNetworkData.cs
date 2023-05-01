using System;
using System.Collections.Generic;
using NeuralNet.Core.Neurons;
using NeuralNet.Core.Neurons.Output;
using UnityEngine;

namespace NeuralNet.Core
{
    [Serializable]
    public class NeuralNetworkData
    {
        public float fitness;
        public List<Neuron> inputNeurons;
        public List<Neuron> hiddenNeurons;
        public List<Neuron> outputNeurons;

        public int nextID;

        public NeuralNetworkData()
        {
            hiddenNeurons = new List<Neuron>();
            inputNeurons = new List<Neuron>();
            outputNeurons = new List<Neuron>();
        }
        
        public Neuron AddNeuron(Vector2 position)
        {
            var newNeuron = new Neuron(nextID, -0.5f, 0.5f, position);
            newNeuron.onChangedNeuronType += OnChangedNeuronType;
            hiddenNeurons.Add(newNeuron);
            nextID++;
            return newNeuron;
        }

        public Weight AddWeight(Neuron fromNeuron, Neuron toNeuron)
        {
            var newWeight = new Weight(fromNeuron.id, toNeuron.id, -0.5f, 0.5f);
            newWeight.AttachNeurons(fromNeuron, toNeuron);
            toNeuron.AddInputWeight(newWeight);
            return newWeight;
        }

        public void RemoveNeuron(Neuron neuron)
        {
            if (hiddenNeurons.Contains(neuron)) hiddenNeurons.Remove(neuron);
            if (inputNeurons.Contains(neuron)) inputNeurons.Remove(neuron);
            if (outputNeurons.Contains(neuron)) outputNeurons.Remove(neuron);
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