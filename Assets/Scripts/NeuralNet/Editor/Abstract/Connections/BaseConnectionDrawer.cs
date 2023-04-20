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
                args.inPointController.model.Rect.center,
                args.outPointController.model.Rect.center,
                args.inPointController.model.Rect.center + Vector2.left * 50f,
                args.outPointController.model.Rect.center - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );
 
            if (Handles.Button((args.inPointController.model.Rect.center + args.outPointController.model.Rect.center) * 0.5f, Quaternion.identity, 4, 8, 
                    Handles.RectangleHandleCap))
            {
                OnClickRemoveConnection?.Invoke();
            }
        }
    }
}