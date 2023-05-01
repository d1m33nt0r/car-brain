using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNet.Core.Activations;
using UnityEngine;

namespace NeuralNet.Core.Neurons.Output
{
    [Serializable]
    public class Neuron
    {
        public event Action<Neuron, NeuronType, NeuronType> onChangedNeuronType; 

        public int id;
        public float data;
        public float bias;
        public NeuronType neuronType;
        public ActivationType activationType;
        public List<Weight> inputWeights;

        public Vector2 position;
        
        public Neuron(int id, float minRange, float maxRange, Vector2 position)
        {
            this.id = id;
            data = default;
            bias = UnityEngine.Random.Range(minRange, maxRange);
            inputWeights = new List<Weight>();
            this.position = position;
            neuronType = NeuronType.Hidden;
            activationType = ActivationType.Sigmoid;
        }

        public void AddInputWeight(Weight weight)
        {
            inputWeights.Add(weight);
        }

        public void RemoveInputWeight(Weight weight)
        {
            inputWeights.Remove(weight);
        }

        public void SetActivationType(ActivationType activationType)
        {
            this.activationType = activationType;
        }

        public void ChangeNeuronType(NeuronType newNeuronType)
        {
            var prevNeuronType = neuronType;
            neuronType = newNeuronType;
            onChangedNeuronType?.Invoke(this, prevNeuronType, newNeuronType);
        }
        
        public void Activate(BrainController controller)
        {
            var weightedSum = inputWeights.Sum(w => w.data * controller.allNeurons[w.inputNeuronID].data);
            var activatedValue = Activation.Instance.Apply(activationType, weightedSum + bias);
            data = activatedValue;
        }
    }
}