namespace NeuralNet.Core.Activations
{
    public class Relu : BaseActivationController
    {
        public Relu(ActivationModel model) : base(model)
        {
        }
        
        public override float Apply(float weightedSum)
        {
            return (0 >= weightedSum) ? 0 : weightedSum;
        }
    }
}