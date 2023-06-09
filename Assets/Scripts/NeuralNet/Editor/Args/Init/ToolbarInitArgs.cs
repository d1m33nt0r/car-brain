using NeuralNet.Editor.Common.Drawers;

namespace NeuralNet.Editor.Args.Init
{
    public class ToolbarInitArgs : BaseArgs
    {
        public VerticalSplitLine verticalSplitLine;

        public ToolbarInitArgs(VerticalSplitLine verticalSplitLine)
        {
            this.verticalSplitLine = verticalSplitLine;
        }
    }
}