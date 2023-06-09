using NeuralNet.Editor.Args;

namespace NeuralNet.Editor.Common.Interfaces
{
    public interface IDrawer<TArgs> where TArgs : BaseArgs
    {
        void Draw(TArgs args);
    }
}