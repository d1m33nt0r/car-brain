using System;
using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Connections
{
    public abstract class BaseConnectionPointDrawer : StylizedDrawer<BaseConnectionPointModel>
    {
        public event Action OnClickConnectionPoint;
        protected abstract override void ApplyStyles();
        
        public abstract override void Draw(BaseConnectionPointModel args);
    }
}