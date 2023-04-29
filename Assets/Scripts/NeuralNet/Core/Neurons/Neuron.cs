using System;
using System.Collections.Generic;
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
        public int activationOrder;
        public NeuronType neuronType;
        public ActivationType activationType;
        public List<Weight> inputWeights;
        public List<Weight> outputWeights;
        
        public Vector2 position;
        
        public Neuron(int id, Vector2 position)
        {
            this.id = id;
            data = default;
            inputWeights = new List<Weight>();
            outputWeights = new List<Weight>();
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
        
        public void AddOutputWeight(Weight weight)
        {
            outputWeights.Add(weight);
        }

        public void RemoveOutputWeight(Weight weight)
        {
            outputWeights.Remove(weight);
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
        
        public void Activate()
        {
            
        }
    }
}