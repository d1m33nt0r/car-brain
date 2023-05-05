using System;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class NodeConnectionPoint : StylizedDrawer<EmptyDrawerArgs>
    {
        public ConnectionPointType type;
        public Node node;
        public Action<NodeConnectionPoint> OnClickConnectionPoint;
    
        protected override void ApplyStyles()
        {
            
        }
        
        public NodeConnectionPoint(Node node, ConnectionPointType type,  Action<NodeConnectionPoint> OnClickConnectionPoint)
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