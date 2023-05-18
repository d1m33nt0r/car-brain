using System;

namespace NeuralNet
{
    [Serializable]
    public struct LearnData
    {
        public float[] expectedInputs;
        public float[] inputParams;

        public LearnData(float[] inputParams, float[] expectedInputs)
        {
            this.expectedInputs = expectedInputs;
            this.inputParams = inputParams;
        }
    }
}