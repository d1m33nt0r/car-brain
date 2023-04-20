using System;
using NeuralNet.Editor.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Connections
{
    public class BaseConnectionDrawer : IDrawer<BaseConnectionModel>
    {
        public event Action OnClickRemoveConnection;
        
        public void Draw(BaseConnectionModel args)
        {
            Handles.DrawBezier(
                args.inPoint.Rect.center,
                args.outPoint.Rect.center,
                args.inPoint.Rect.center + Vector2.left * 50f,
                args.outPoint.Rect.center - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );
 
            if (Handles.Button((args.inPoint.Rect.center + args.outPoint.Rect.center) * 0.5f, Quaternion.identity, 4, 8, 
                    Handles.RectangleHandleCap))
            {
                OnClickRemoveConnection?.Invoke();
            }
        }
    }
}