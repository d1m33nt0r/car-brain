using NeuralNet.Editor.Common.Drawers;

namespace NeuralNet.Editor.Args.Init
{
    public class HorizontalLineInitArgs : BaseArgs
    {
        public VerticalSplitLine splitLine;

        public HorizontalLineInitArgs(VerticalSplitLine splitLine)
        {
            this.splitLine = splitLine;
        }
    }
}