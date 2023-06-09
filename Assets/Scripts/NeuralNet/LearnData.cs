using System;

namespace NeuralNet
{
    [Serializable]
    public struct LearnData
    {
        public double[] expectedInputs;
        public double[] inputParams;

        public LearnData(double[] inputParams, double[] expectedInputs)
        {
            this.expectedInputs = expectedInputs;
            this.inputParams = inputParams;
        }
    }
}