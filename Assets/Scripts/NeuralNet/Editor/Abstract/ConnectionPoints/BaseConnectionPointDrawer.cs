using System;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public abstract class BaseConnectionPointDrawer : StylizedDrawer<BaseConnectionPointModel>
    {
        public abstract event Action<ConnectionPointType> OnClickConnectionPoint;
        protected abstract override void ApplyStyles();
        
        public abstract override void Draw(BaseConnectionPointModel args);
    }
}