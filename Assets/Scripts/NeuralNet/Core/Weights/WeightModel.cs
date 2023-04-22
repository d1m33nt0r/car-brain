using System;
using NeuralNet.Core.Abstract;

namespace NeuralNet.Core
{
    [Serializable]
    public class WeightModel : BaseModel
    {
        public int neuronIndex;
        public float data;
    }
}