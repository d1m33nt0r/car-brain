using NeuralNet.Editor.Common.Drawers;

namespace NeuralNet.Editor.Args.Init
{
    public class TestViewInitArgs : BaseArgs
    {
        public HorizontalSplitLine horizontalSplitLine;
        public VerticalSplitLine verticalSplitLine;
        
        public TestViewInitArgs(HorizontalSplitLine horizontalSplitLine, VerticalSplitLine verticalSplitLine)
        {
            this.horizontalSplitLine = horizontalSplitLine;
            this.verticalSplitLine = verticalSplitLine;
        }
    }
}