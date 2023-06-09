using NeuralNet.Editor.Args;

namespace NeuralNet.Editor.Common.Interfaces
{
    public interface IInitializable<TSelfArgs> where TSelfArgs : BaseArgs
    {
        public void Initialize(TSelfArgs args);
    }
}