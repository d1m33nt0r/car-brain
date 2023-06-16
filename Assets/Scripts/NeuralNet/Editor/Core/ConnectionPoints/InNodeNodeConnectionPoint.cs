using System;
using NeuralNet.Editor.Args;
using UnityEngine;

namespace NeuralNet.Editor.Core.ConnectionPoints
{
    public class InNodeNodeConnectionPoint : NodeConnectionPoint
    {
        public InNodeNodeConnectionPoint(Node node, ConnectionPointType type, Action<NodeConnectionPoint> OnClickConnectionPoint) 
            : base(node, type, OnClickConnectionPoint)
        {
        }
        
        public override void Draw(EmptyArgs args)
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;
            rect.x = node.rect.x - rect.width;
            
            if (GUI.Button(rect, "", styles[IN_CONNECTION_POINT_STYLE]))
            {
                if (OnClickConnectionPoint != null)
                {
                    OnClickConnectionPoint(this);
                }
            }
        }
    }
}