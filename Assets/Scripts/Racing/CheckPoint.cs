using NeuralNet;
using UnityEngine;

namespace DefaultNamespace.Racing
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private float weight;
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Car"))
            {
                other.GetComponentInChildren<NeuralBehaviour>().AddFitness(weight);
            }
        }
    }
}