using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace NeuralNet.Core
{
    public static class Serializer
    {
        private static void Sort(NeuralNetworkData nnd)
        {
            var sorted = nnd.hiddenNeurons.OrderByDependers(n => n.inputWeights.Select(w => w.inputNeuron).ToList()).ToList();
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
            var networkData = JsonUtility.FromJson<NeuralNetworkData>(jsonString);
            return networkData;
        }
    }
}