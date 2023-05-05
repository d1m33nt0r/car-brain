using System;
using NeuralNet.Core;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class Connection : StylizedDrawer<EmptyDrawerArgs>
    {
        public Weight weight;
        public NodeConnectionPoint inPoint;
        public NodeConnectionPoint outPoint;
        public Action<Connection> OnClickRemoveConnection;
 
        public Connection(Weight weight, NodeConnectionPoint inPoint, NodeConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
        {
            this.weight = weight;
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
        }

        protected override void ApplyStyles()
        {
            
        }

        public override void Draw(EmptyDrawerArgs args)
        {
            var startPoint = inPoint.rect.center;
            var endPoint = outPoint.rect.center;
            var startTangent = startPoint + Vector2.right * -50;
            var endTangent = endPoint - Vector2.right * -50;
            
            Handles.DrawBezier(startPoint, endPoint, startTangent,endTangent, Color.black, null, 3f);
            
            var bezierPoints = Handles.MakeBezierPoints(startPoint, endPoint, startTangent, endTangent, 7);

            Vector2 button = bezierPoints[2];
            var arrowPoint = bezierPoints[4];
            var centerPoint = bezierPoints[3];
            var cross = Vector3.Cross((endPoint - startPoint).normalized, Vector3.forward);
            var diff = (startPoint - endPoint);
            var direction = diff.normalized;

            new DirectionArrow().DrawArrow(cross, direction, button, Color.black);
            
            //Handles.DrawSolidDisc(t, Vector3.forward, 15);
            // Малюємо круг з полем для введення числа в його центрі
            EditorGUI.BeginChangeCheck();
            weight.data = EditorGUI.FloatField(new Rect(centerPoint.x - 30f, centerPoint.y - 10f, 60f, 20f), weight.data);
            EditorGUI.EndChangeCheck();
            

            if (Handles.Button(arrowPoint, Quaternion.identity, 4, 8, Handles.CircleHandleCap))
            {
                if (OnClickRemoveConnection != null)
                {
                    OnClickRemoveConnection(this);
                }
            }
        }
    }
}