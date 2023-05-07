using System;
using NaughtyAttributes;
using NeuralNet.Core.Activations;
using UnityEngine;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private ActivationType activationType;
        [SerializeField] private float weightedSum;
        [SerializeField] private float bias;

        [Button("Get Activated Value")]
        private void Activate()
        { 
            
        }
        
        [Button("Get Activated Value 2")]
        public void Apply()
        {
            var k = (double)Math.Exp(weightedSum);
            Debug.Log(k / (1.0f + k));
        }

        [Button("Test")]
        public void Tests()
        {
            Debug.Log(Math.Pow(3.3f - 0.5f, 2));
        }
    }
}