using System;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class InNodeNodeConnectionPoint : NodeConnectionPoint
    {
        public InNodeNodeConnectionPoint(Node node, ConnectionPointType type, Action<NodeConnectionPoint> OnClickConnectionPoint) : base(node, type, OnClickConnectionPoint)
        {
        }

        protected override void ApplyStyles()
        {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            style.border = new RectOffset(4, 4, 12, 12);
        }

        public override void Draw(EmptyDrawerArgs args)
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;
            rect.x = node.rect.x - rect.width;
            base.Draw(args);
        }
    }
}