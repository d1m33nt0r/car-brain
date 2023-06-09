using System;
using System.Collections.Generic;
using NeuralNet.Core;
using NeuralNet.Core.Neurons.Output;
using UnityEngine;

namespace NeuralNet.Editor
{
    [Serializable]
    public class EditorNeuralNetworkData
    {
        public string brainName;
        public float fitness;
        public List<Neuron> inputNeurons;
        public List<Neuron> hiddenNeurons;
        public List<Neuron> outputNeurons;
        public List<int> allNeurons;
        public int nextID;

        public NeuralNetworkData ToScriptableObject()
        {
            var brainAsset = ScriptableObject.CreateInstance<NeuralNetworkData>();
            brainAsset.fitness = fitness;
            brainAsset.inputNeurons = inputNeurons;
            brainAsset.hiddenNeurons = hiddenNeurons;
            brainAsset.outputNeurons = outputNeurons;
            brainAsset.allNeurons = allNeurons;
            brainAsset.nextID = nextID;
            return brainAsset;
        }
    }
}