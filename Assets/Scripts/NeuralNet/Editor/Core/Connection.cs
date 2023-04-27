using System;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class Connection
    {
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;
        public Action<Connection> OnClickRemoveConnection;
 
        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
        }
 
        public void Draw()
        {
            var startPoint = inPoint.rect.center;
            var endPoint = outPoint.rect.center;
            var startTangent = startPoint + Vector2.right * -50f;
            var endTangent = endPoint - Vector2.right * -50f;
            
            Handles.DrawBezier(startPoint, endPoint, startTangent,endTangent, Color.black, null, 3f);
            
            var bezierPoints = Handles.MakeBezierPoints(startPoint, endPoint, startTangent, endTangent, 7);

            Vector2 middlePoint = bezierPoints[2];
            var s = bezierPoints[3];
            var t = bezierPoints[3];
            var cross = Vector3.Cross((endPoint - startPoint).normalized, Vector3.forward);
            var diff = (startPoint - endPoint);
            var direction = diff.normalized;

            new DirectionArrow().DrawArrow(cross, direction, middlePoint, Color.black);
            /*
            //Handles.DrawSolidDisc(t, Vector3.forward, 15);
            // Малюємо круг з полем для введення числа в його центрі
            EditorGUI.BeginChangeCheck();
            EditorGUI.FloatField(new Rect(t.x - 30f, t.y - 10f, 60f, 20f), 50);
            EditorGUI.EndChangeCheck();
            */

            if (Handles.Button(s, Quaternion.identity, 4, 8, Handles.CircleHandleCap))
            {
                if (OnClickRemoveConnection != null)
                {
                    OnClickRemoveConnection(this);
                }
            }
        }
    }
}