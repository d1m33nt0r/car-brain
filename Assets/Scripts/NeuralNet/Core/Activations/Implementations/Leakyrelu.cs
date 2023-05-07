namespace NeuralNet.Core.Activations
{
    public class Leakyrelu : BaseActivationController
    {
        public Leakyrelu(ActivationModel model) : base(model)
        {
        }
        
        public override float Apply(float weightedSum)
        {
            return (0 >= weightedSum) ? 0.01f * weightedSum : weightedSum;
        }

        public override float Derivative(float x)
        {
            return default;
        }
    }
}