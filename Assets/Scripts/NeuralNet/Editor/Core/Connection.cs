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
            Handles.DrawBezier(
                inPoint.rect.center,
                outPoint.rect.center,
                inPoint.rect.center + Vector2.right * -50f,
                outPoint.rect.center - Vector2.right * -50f,
                Color.white,
                null,
                2f
            );
 
            Vector2 startPoint = inPoint.rect.center;
            Vector2 endPoint = outPoint.rect.center;
            Vector2 startTangent = startPoint + Vector2.right * -50f;
            Vector2 endTangent = endPoint - Vector2.right * -50f;
            float thickness = 2f;
            

            Vector3[] bezierPoints = Handles.MakeBezierPoints(startPoint, endPoint, startTangent, endTangent, 10);
            
            Vector2 middlePoint = bezierPoints[2];
            
            float arrowSize = 10f;
            Handles.DrawAAPolyLine(thickness, middlePoint, middlePoint + arrowSize * (startPoint - endPoint).normalized);
            Handles.DrawAAPolyLine(thickness, middlePoint, middlePoint + arrowSize * (endPoint - startPoint).normalized);
            
            if (Handles.Button(middlePoint, Quaternion.identity, 4, 8, Handles.CircleHandleCap))
            {
                if (OnClickRemoveConnection != null)
                {
                    OnClickRemoveConnection(this);
                }
            }
        }
    }
}