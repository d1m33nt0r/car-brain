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

        public static string WriteToJson(NeuralNetworkData neuralNetworkData)
        {
            Sort(neuralNetworkData);
            var json = JsonUtility.ToJson(neuralNetworkData);
            return json;
        }

        public static NeuralNetworkData ReadFromJson(string path)
        {
            var networkData = JsonUtility.FromJson<NeuralNetworkData>(path);
            return networkData;
        }
    }
}