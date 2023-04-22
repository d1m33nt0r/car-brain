using NeuralNet.Core.Abstract;
using NeuralNet.Core.Activations;
using NeuralNet.Core.Common;

namespace NeuralNet.Core.Services.Factories
{
    public class SigmoidFactory : IFactory<Sigmoid, FunctionActivationModel, EmptyArgs>
    {
        public Sigmoid Create(EmptyArgs args)
        {
            var sigmoidModel = new FunctionActivationModel() { Type = ActivationType.Sigmoid };
            return new Sigmoid(sigmoidModel);
        }
    }
}