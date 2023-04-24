using UnityEngine;

namespace NeuralNet.Editor.Abstract
{
    public abstract class StylizedDrawer<TModel> : IDrawer<TModel> where TModel : BaseModel
    {
        protected GUIStyle style;
        protected abstract void ApplyStyles();

        protected StylizedDrawer()
        {
            ApplyStyles();
        }
        
        public abstract void Draw(TModel args);
    }
}