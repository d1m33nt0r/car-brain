using UnityEngine;

namespace NeuralNet.Editor.Args.StyleArgs
{
    public class BrainCollectionViewStyleArgs : BaseArgs
    {
        public Color foldoutColor { get; }

        public BrainCollectionViewStyleArgs(Color color)
        {
            foldoutColor = color;
        }
    }
}