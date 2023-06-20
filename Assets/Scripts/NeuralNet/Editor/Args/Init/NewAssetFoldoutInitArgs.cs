using NeuralNet.Editor.Template.LeftSection.Foldouts;

namespace NeuralNet.Editor.Args.Draw
{
    public class NewAssetFoldoutInitArgs : BaseArgs
    {
        public FileFoldout fileFoldout { get; private set; }

        public NewAssetFoldoutInitArgs(FileFoldout fileFoldout)
        {
            this.fileFoldout = fileFoldout;
        }
    }
}