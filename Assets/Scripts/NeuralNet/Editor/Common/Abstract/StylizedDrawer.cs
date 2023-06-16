using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Interfaces;
using UnityEngine;

namespace NeuralNet.Editor.Common.Abstract
{
    public abstract class StylizedDrawer<TStyleArgs, TDrawArgs> : IDrawer<TDrawArgs> where TDrawArgs : BaseArgs
    {
        public Rect rect = new ();
        protected Dictionary<string, GUIStyle> styles = new ();
        protected abstract void ApplyStyles(TStyleArgs styleArgs);

        public StylizedDrawer(TStyleArgs styleArgs = default)
        {
            ApplyStyles(styleArgs);
        }

        public abstract void Draw(TDrawArgs args);
    }
}