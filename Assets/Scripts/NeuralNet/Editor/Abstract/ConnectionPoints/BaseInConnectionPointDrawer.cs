using System;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Connections
{
    public class BaseInConnectionPointDrawer : BaseConnectionPointDrawer
    {
        public override event Action<ConnectionPointType> OnClickConnectionPoint;

        protected override void ApplyStyles()
        {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            style.border = new RectOffset(4, 4, 12, 12);
        }

        public override void Draw(BaseConnectionPointModel args)
        {
            args.Rect.y = args.BaseNodeModel.Rect.y + (args.BaseNodeModel.Rect.height * 0.5f) - args.Rect.height * 0.5f;
            args.Rect.x = args.BaseNodeModel.Rect.x - args.Rect.width + 8f;
            
            if (GUI.Button(args.Rect, "", style))
            {
                OnClickConnectionPoint?.Invoke(args.ConnectionPointType);
            }
        }
    }
}