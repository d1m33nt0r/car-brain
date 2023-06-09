using NeuralNet.Editor.Common;

namespace NeuralNet.Editor.Args.Init
{
    public class GraphInitArgs : BaseArgs
    {
        public GlobalState State { get; }
        public GraphInitArgs(GlobalState state)
        {
            State = state;
        }
    }
}