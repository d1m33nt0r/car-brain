using System;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class ConnectionPoint : StylizedDrawer<EmptyDrawerArgs>
    {
        public ConnectionPointType type;
        public Node node;
        public Action<ConnectionPoint> OnClickConnectionPoint;
    
        protected override void ApplyStyles()
        {
            
        }
        
        public ConnectionPoint(Node node, ConnectionPointType type,  Action<ConnectionPoint> OnClickConnectionPoint)
        {
            this.node = node;
            this.type = type;
            this.OnClickConnectionPoint = OnClickConnectionPoint;
            rect = new Rect(0, 0, 10f, 20f);
        }
 
        public override void Draw(EmptyDrawerArgs args)
        {
            if (GUI.Button(rect, "", style))
            {
                if (OnClickConnectionPoint != null)
                {
                    OnClickConnectionPoint(this);
                }
            }
        }
    }
}