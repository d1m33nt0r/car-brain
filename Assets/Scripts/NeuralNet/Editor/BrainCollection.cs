using System.Collections.Generic;
using NeuralNet.Core;
using UnityEngine;

namespace NeuralNet.Editor
{
    [CreateAssetMenu(fileName = "Collection", menuName = "Collection")]
    public class BrainCollection : ScriptableObject
    {
        public string name;
        public List<BrainData> data;
    }
}