using System;
using UnityEngine;

namespace NeuralNet.Core.Neurons.Abstract
{
    [Serializable]
    public class BaseNeuron
    {
        public float data;
        public Vector2 position;
        public BaseNeuron(float data, Vector2 position)
        {
            this.data = data;
        }
    }
}