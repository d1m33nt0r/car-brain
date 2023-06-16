using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace NeuralNet.Core
{
    public static class Serializer
    {
        private static void Sort(BrainData nnd)
        {
            var sorted = nnd.hiddenNeurons.OrderByDependers(n => n.inputWeights.Select(w => w.inputNeuron).ToList()).ToList();
            sorted.Reverse();
            nnd.hiddenNeurons = sorted;
        }

        public static void WriteToJson(string path, BrainData brainData, bool needToSort)
        {
            if (needToSort) Sort(brainData);
            var json = JsonUtility.ToJson(brainData);
            File.WriteAllText(path, json);
        }

        public static BrainData ReadFromJson(string path)
        {
            var jsonString = File.ReadAllText(path);
            var networkData = JsonUtility.FromJson<BrainData>(jsonString);
            return networkData;
        }
    }
}