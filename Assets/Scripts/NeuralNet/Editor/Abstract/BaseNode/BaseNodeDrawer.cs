using NeuralNet.Editor.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Nodes.BaseNode
{
    public class BaseNodeDrawer : StylizedDrawer<BaseNodeModel>
    {
        protected override void ApplyStyles()
        {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            style.border = new RectOffset(12, 12, 12, 12);
        }
        
        public override void Draw(BaseNodeModel args)
        {
            args.inPointDrawer.Draw(args.inPointModel);
            args.outPointDrawer.Draw(args.outPointModel);
            GUI.Box(args.Rect, args.Title, style);
        }
    }
}