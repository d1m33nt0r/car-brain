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
        public List<int> inputNeuronsIDs;
        public List<int> outputNeuronsIDs;
        public List<Neuron> allNeurons;
        public int nextID;

        public NeuralNetworkData()
        {
            allNeurons = new List<Neuron>();
            inputNeuronsIDs = new List<int>();
            outputNeuronsIDs = new List<int>();
        }
        
        public Neuron AddNeuron(Vector2 position)
        {
            var newNeuron = new Neuron(nextID, position);
            newNeuron.onChangedNeuronType += OnChangedNeuronType;
            allNeurons.Add(newNeuron);
            nextID++;
            return newNeuron;
        }

        public Weight AddWeight(Neuron fromNeuron, Neuron toNeuron)
        {
            var newWeight = new Weight(fromNeuron, toNeuron);
            toNeuron.inputWeights.Add(newWeight);
            fromNeuron.outputWeights.Add(newWeight);
            return newWeight;
        }

        public void RemoveNeuron(Neuron neuron)
        {
            allNeurons.Remove(neuron);
            if (inputNeuronsIDs.Contains(neuron.id)) inputNeuronsIDs.Remove(neuron.id);
            if (outputNeuronsIDs.Contains(neuron.id)) outputNeuronsIDs.Remove(neuron.id);
        }

        public void RemoveWeight(Weight weight)
        {
            weight.inputNeuron.outputWeights.Remove(weight);
            weight.outputNeuron.inputWeights.Remove(weight);
        }

        private void OnChangedNeuronType(Neuron neuron, NeuronType prevNeuronType, NeuronType newNeuronType)
        {
            if (prevNeuronType == NeuronType.Hidden && newNeuronType == NeuronType.Input)
            {
                inputNeuronsIDs.Add(neuron.id);
            }

            if (prevNeuronType == NeuronType.Hidden && newNeuronType == NeuronType.Output)
            {
                outputNeuronsIDs.Add(neuron.id);
            }

            if (prevNeuronType == NeuronType.Input)
            {
                inputNeuronsIDs.Remove(neuron.id);
                if (newNeuronType == NeuronType.Output)
                {
                    outputNeuronsIDs.Add(neuron.id);
                }
            }

            if (prevNeuronType == NeuronType.Output)
            {
                outputNeuronsIDs.Remove(neuron.id);
                if (newNeuronType == NeuronType.Input)
                {
                    inputNeuronsIDs.Add(neuron.id);
                }
            }
        }
    }
}