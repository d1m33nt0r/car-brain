using System.IO;

namespace NeuralNet
{
    public partial class NeuralNetwork
    {
        public void Save(string path) //this is used for saving the biases and weights within the network to a file.
        {
            File.Create(path).Close();
            StreamWriter writer = new StreamWriter(path, true);
            
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    writer.WriteLine(biases[i][j]);
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        writer.WriteLine(weights[i][j][k]);
                    }
                }
            }

            writer.Close();
        }

        public void Load(string path) //this loads the biases and weights from within a file into the neural network.
        {
            TextReader tr = new StreamReader(path);
            int NumberOfLines = (int)new FileInfo(path).Length;
            string[] ListLines = new string[NumberOfLines];
            int index = 1;
            for (int i = 1; i < NumberOfLines; i++)
            {
                ListLines[i] = tr.ReadLine();
            }

            tr.Close();
            if (new FileInfo(path).Length > 0)
            {
                for (int i = 0; i < biases.Length; i++)
                {
                    for (int j = 0; j < biases[i].Length; j++)
                    {
                        biases[i][j] = float.Parse(ListLines[index]);
                        index++;
                    }
                }

                for (int i = 0; i < weights.Length; i++)
                {
                    for (int j = 0; j < weights[i].Length; j++)
                    {
                        for (int k = 0; k < weights[i][j].Length; k++)
                        {
                            weights[i][j][k] = float.Parse(ListLines[index]);
                            index++;
                        }
                    }
                }
            }
        }
    }
}