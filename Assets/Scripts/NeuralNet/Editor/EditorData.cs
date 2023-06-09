using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class EditorData : ScriptableObject
    {
        public List<BrainCollection> collections;
    }

    [Serializable]
    public class BrainCollection
    {
        public string name;
        public string path;
    }
}