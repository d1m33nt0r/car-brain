using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Drawers;

namespace NeuralNet.Editor.Args.Init
{
    public class SidebarInitArgs : BaseArgs
    {
        public VerticalSplitLine VerticalSplitLine { get; }
        public GlobalState State { get; }
        public SidebarInitArgs(VerticalSplitLine verticalSplitLine, GlobalState state)
        {
            VerticalSplitLine = verticalSplitLine;
            State = state;
        }
    }
}