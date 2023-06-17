using NeuralNet.Core;

namespace NeuralNet.Editor.Args.Init
{
    public class BrainViewInitArgs : BaseArgs
    {
        public BrainData brainAsset { get; }

        public BrainViewInitArgs(BrainData brainAsset)
        {
            this.brainAsset = brainAsset;
        }
    }
}