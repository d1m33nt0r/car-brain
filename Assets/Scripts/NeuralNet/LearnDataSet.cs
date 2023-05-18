using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeuralNet
{
    [CreateAssetMenu(menuName = "NeuralNet/Learn Data", fileName = "LearnDataSet")]
    public class LearnDataSet : ScriptableObject
    {
        public List<LearnData> data;
    }
}