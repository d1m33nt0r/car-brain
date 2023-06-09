using System.IO;
using System.Linq;
using NeuralNet.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace NeuralNet.Editor
{
    public static class EditorSerializer
    {
        private static void Sort(NeuralNetworkData nnd)
        {
            var sorted = nnd.hiddenNeurons.OrderByDependers(n => n.inputWeights.Select(w => w.inputNeuron).ToList())
                .ToList();
            sorted.Reverse();
            nnd.hiddenNeurons = sorted;
        }

        public static void WriteToJson(string path, NeuralNetworkData neuralNetworkData, bool needToSort)
        {
            if (needToSort) Sort(neuralNetworkData);
            var json = JsonUtility.ToJson(neuralNetworkData);
            File.WriteAllText(path, json);
        }

        public static NeuralNetworkData ReadFromJson(string path)
        {
            var jsonString = File.ReadAllText(path);
            var networkData = JsonUtility.FromJson<EditorNeuralNetworkData>(jsonString);
            return networkData.ToScriptableObject();
        }
    }
}