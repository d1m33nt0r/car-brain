using UnityEngine;

namespace NeuralNet.Editor
{
    public abstract class StylizedDrawer<T> : IDrawer<T> where T : BaseDrawerArgs
    {
        public Rect rect;
        protected GUIStyle style;
        protected abstract void ApplyStyles();

        public StylizedDrawer()
        {
            ApplyStyles();
        }

        public abstract void Draw(T args);
    }
}