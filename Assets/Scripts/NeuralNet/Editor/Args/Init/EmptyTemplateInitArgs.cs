using NeuralNet.Editor.Template.LeftSection.Foldouts;

namespace NeuralNet.Editor.Args.Init
{
    public class EmptyTemplateInitArgs : BaseArgs
    {
        public NewAssetFoldout newAssetFoldout { get; }

        public EmptyTemplateInitArgs(NewAssetFoldout newAssetFoldout)
        {
            this.newAssetFoldout = newAssetFoldout;
        }
    }
}