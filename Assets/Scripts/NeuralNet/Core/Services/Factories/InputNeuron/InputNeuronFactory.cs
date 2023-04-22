using NeuralNet.Core.Abstract;

namespace NeuralNet.Core.Services.Factories
{
    public class InputNeuronFactory : IFactory<InputNeuronController<InputNeuronModel>, InputNeuronModel, InputNeuronFactoryArgs>
    {
        public InputNeuronController<InputNeuronModel> Create(InputNeuronFactoryArgs args)
        {
            return new InputNeuronController<InputNeuronModel>(new InputNeuronModel(args.data));
        }
    }
}