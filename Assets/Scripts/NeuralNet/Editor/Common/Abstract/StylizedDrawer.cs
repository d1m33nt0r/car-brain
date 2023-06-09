using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Interfaces;
using UnityEngine;

namespace NeuralNet.Editor.Common.Abstract
{
    public abstract class StylizedDrawer<T> : IDrawer<T> where T : BaseArgs
    {
        public Rect rect = new ();
        protected Dictionary<string, GUIStyle> styles = new ();
        protected abstract void ApplyStyles();

        public StylizedDrawer()
        {
            ApplyStyles();
        }

        public abstract void Draw(T args);
    }
}