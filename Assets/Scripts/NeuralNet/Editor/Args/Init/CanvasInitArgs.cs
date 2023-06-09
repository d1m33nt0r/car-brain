using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Drawers;

namespace NeuralNet.Editor.Args.Init
{
    public class CanvasInitArgs : BaseArgs
    {
        public VerticalSplitLine verticalSplitLine;
        public GlobalState State;
        
        public CanvasInitArgs(VerticalSplitLine verticalSplitLine, GlobalState state)
        {
            this.verticalSplitLine = verticalSplitLine;
            State = state;
        }
    }
}