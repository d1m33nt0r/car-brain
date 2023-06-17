using System.Collections.Generic;
using UnityEngine;

namespace NeuralNet.Editor
{
    [CreateAssetMenu(fileName = "EditorData", menuName = "EditorData")]
    public class EditorData : ScriptableObject
    {
        public List<BrainCollection> collections;
    }
}