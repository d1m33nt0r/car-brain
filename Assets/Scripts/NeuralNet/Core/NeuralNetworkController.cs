namespace NeuralNet.Core
{
    public class NeuralNetworkController
    {
        private NeuralNetworkModel model;
        
        public NeuralNetworkController(NeuralNetworkModel neuralNetworkModel)
        {
            model = neuralNetworkModel;
        }
        
        public float[] FeedForward(float[] inputs)//feed forward, inputs >==> outputs.
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < layers[i]; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < layers[i - 1]; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = activate(value + biases[i-1][j], layer);
                }
            }
            return neurons[layers.Length-1];
        }
    }
}