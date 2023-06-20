using NeuralNet.Editor.Template;

namespace NeuralNet.Editor.Args.Draw
{
    public class LayerFoldoutDrawArg : BaseArgs
    {
        public NewAssetData data { get; }
        public int index { get; }
        
        public LayerFoldoutDrawArg(NewAssetData data, int index)
        {
            this.data = data;
            this.index = index;
        }
    }
}