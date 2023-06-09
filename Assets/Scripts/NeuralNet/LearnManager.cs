using NaughtyAttributes;
using NeuralNet.Core;
using UnityEngine;

namespace NeuralNet
{
    [ExecuteAlways]
    public class LearnManager : MonoBehaviour
    {
        public LearnDataSet LearnDataSet => learnDataSet;
        [SerializeField] private LearnDataSet learnDataSet;
        [SerializeField] private string learnedAssetPath;
        [SerializeField] private string assetPathToLearning;
        [SerializeField] private float learningRate;
        
        [Button("Learn")]
        private void Learn()
        {
            var brain = new BrainController(assetPathToLearning, false);
            Debug.Log("Learning started");
            
            for (var i = 0; i < learnDataSet.data.Count; i++)
            {
                brain.BackPropagation(learnDataSet.data[i].inputParams, learnDataSet.data[i].expectedInputs, learningRate);
            }
            brain.Save(learnedAssetPath);
            
            Debug.Log("Learning finished");
        }
    }
}