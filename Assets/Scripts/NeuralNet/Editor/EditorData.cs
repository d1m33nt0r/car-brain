using System.Collections.Generic;
using NeuralNet.Core;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class EditorData : ScriptableObject
    {
        public List<BrainCollection> collections;
    }
    
    public class BrainCollection : ScriptableObject
    {
        public string name;
        public List<BrainData> data;
    }
}