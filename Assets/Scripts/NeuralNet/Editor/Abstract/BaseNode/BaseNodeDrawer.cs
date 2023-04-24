using NeuralNet.Editor.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Nodes.BaseNode
{
    public class BaseNodeDrawer : StylizedDrawer<BaseNodeModel>
    {
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;
        
        protected override void ApplyStyles()
        {
            defaultNodeStyle = new GUIStyle();
            defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);;
            defaultNodeStyle.alignment = TextAnchor.UpperCenter;
            defaultNodeStyle.contentOffset = new Vector2(0, -15);

            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);
            selectedNodeStyle.alignment = TextAnchor.UpperCenter;
            selectedNodeStyle.contentOffset = new Vector2(0, -15);
            
            ChangeStyle(false);
        }
        
        public override void Draw(BaseNodeModel args)
        {
            args.inPointDrawer.Draw(args.inPointModel);
            args.outPointDrawer.Draw(args.outPointModel);
            GUI.Box(args.Rect, args.Title, style);
        }

        public void ChangeStyle(bool isFocusedNodeStyle)
        {
            style = isFocusedNodeStyle ? selectedNodeStyle : defaultNodeStyle;
        }
    }
}