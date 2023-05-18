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
        public double data;
        public double delta;
        public float bias;
        public NeuronType neuronType;
        public ActivationType activationType;
        public List<Weight> inputWeights;
        public List<Weight> outputWeights;
        public Vector2 position;
        
        public Neuron(int id, float minRange, float maxRange, Vector2 position)
        {
            this.id = id;
            data = default;
            bias = UnityEngine.Random.Range(minRange, maxRange);
            inputWeights = new List<Weight>();
            outputWeights = new List<Weight>();
            this.position = position;
            neuronType = NeuronType.Hidden;
            activationType = ActivationType.Sigmoid;
        }

        public Neuron()
        {
            
        }

        public Neuron Copy()
        {
            var weights = new List<Weight>();

            for (var i = 0; i < inputWeights.Count; i++)
            {
                weights.Add(inputWeights[i].Copy());
            }

            return new Neuron()
            {
                id = id,
                activationType = activationType,
                bias = bias,
                data = data,
                inputWeights = weights,
                neuronType = neuronType,
                onChangedNeuronType = onChangedNeuronType,
                position = position
            };
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
        
        public void Activate(BrainController controller)
        {
            var weightedSum = inputWeights.Sum(w => w.data * controller.allNeurons[w.inputNeuronID].data);
            var activatedValue = Activation.Instance.Apply(activationType, weightedSum + bias);
            data = activatedValue;
        }

        public double Derivative()
        {
            return Activation.Instance.Derivative(activationType, data);
        }
    }
}