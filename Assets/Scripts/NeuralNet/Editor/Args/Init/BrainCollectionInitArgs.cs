namespace NeuralNet.Editor.Args.Init
{
    public class BrainCollectionInitArgs : BaseArgs
    {
        public BrainCollection brainCollection { get; }

        public BrainCollectionInitArgs(BrainCollection brainCollection)
        {
            this.brainCollection = brainCollection;
        }
    }
}