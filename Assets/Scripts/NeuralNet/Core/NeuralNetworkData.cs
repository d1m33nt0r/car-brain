using System.Collections.Generic;
using NeuralNet.Core.Neurons;
using NeuralNet.Core.Neurons.Output;
using UnityEngine;

namespace NeuralNet.Core
{
    [CreateAssetMenu(fileName = "BrainEditorAsset", menuName = "NeuralNet/Brain Asset")]
    public class NeuralNetworkData : ScriptableObject
    {
        public SerializableNeuronDictionary allNeurons;

        [HideInInspector] public List<int> inputNeuronsIDs;
        [HideInInspector] public List<int> outputNeuronsIDs;
        [HideInInspector, SerializeField] private int nextID;

        public void AddNeuron(Vector2 position)
        {
            var newNeuron = new Neuron(nextID, position);
            newNeuron.onChangedNeuronType += OnChangedNeuronType;
            allNeurons.Add(nextID, newNeuron);
            nextID++;
        }

        public void AddWeight(int inputNeuronID, int outputNeuronID)
        {
            var newWeight = new Weight { inputNeuronID = inputNeuronID, outputNeuronID = outputNeuronID };
            allNeurons[outputNeuronID].inputWeights.Add(newWeight);
            allNeurons[inputNeuronID].outputWeights.Add(newWeight);
        }

        public void RemoveNeuron(Neuron neuron)
        {
            allNeurons.Remove(neuron.id);
            if (inputNeuronsIDs.Contains(neuron.id)) inputNeuronsIDs.Remove(neuron.id);
            if (outputNeuronsIDs.Contains(neuron.id)) outputNeuronsIDs.Remove(neuron.id);
        }

        public void RemoveWeight(Weight weight)
        {
            allNeurons[weight.inputNeuronID].outputWeights.Remove(weight);
            allNeurons[weight.outputNeuronID].inputWeights.Remove(weight);
        }

        public bool TryGetNeuron(int neuronID, out Neuron neuron)
        {
            if (allNeurons.ContainsKey(neuronID))
            {
                neuron = allNeurons[neuronID];
                return true;
            }

            neuron = null;
            return false;
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