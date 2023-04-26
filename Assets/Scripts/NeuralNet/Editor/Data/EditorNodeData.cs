using System;
using UnityEngine;

namespace NeuralNet.Editor.Data
{
    [Serializable]
    public class EditorNodeData
    {
        public NodeType type;
        public int nodeID;
        public Vector2 position;
    }
}