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
                args.inPointController.model.Rect.center + Vector2.right * 50f,
                args.outPointController.model.Rect.center - Vector2.right * 50f,
                new Color(0.1f, 0.1f, 0.1f, 0.85f),
                null,
                3f
            );
            
            /*Vector3 cross = Vector3.Cross((args.outPointController.model.Rect.center - args.inPointController.model.Rect.center).normalized, Vector3.forward);
            Vector3 diff = (args.inPointController.model.Rect.center - args.outPointController.model.Rect.center);
            Vector3 direction = diff.normalized;
            Vector3 mid = ((0.5f * diff) + (Vector3)args.outPointController.model.Rect.center) - (0.5f * cross);
            Vector3 center = mid + direction;
       
            new DirectionArrow().DrawArrow(cross, direction, center, new Color(0.1f, 0.1f, 0.1f, 0.75f));*/

            var pos = (args.inPointController.model.Rect.center + args.outPointController.model.Rect.center) * 0.5f;
            var button = Handles.Button(pos,
                Quaternion.identity, 4, 8,
                Handles.RectangleHandleCap);
            
            if (button)
            {
                OnClickRemoveConnection?.Invoke();
            }
        }
    }
}