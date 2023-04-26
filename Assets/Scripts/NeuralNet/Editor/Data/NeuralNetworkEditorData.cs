using System.Collections.Generic;
using UnityEngine;

namespace NeuralNet.Editor.Data
{
    [CreateAssetMenu(fileName = "BrainEditorData", menuName = "NeuralNet/Brain Editor Data", order = 0)]
    public class NeuralNetworkEditorData : ScriptableObject
    {
        public List<EditorNodeData> data;
    }
}