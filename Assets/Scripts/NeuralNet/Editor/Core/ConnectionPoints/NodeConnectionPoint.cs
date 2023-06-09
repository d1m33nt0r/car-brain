using System;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Core.ConnectionPoints
{
    public abstract class NodeConnectionPoint : StylizedDrawer<EmptyArgs>
    {
        protected const string IN_CONNECTION_POINT_STYLE = "inConnectionPoint";
        protected const string OUT_CONNECTION_POINT_STYLE = "outConnectionPoint";
        public ConnectionPointType type;
        public Node node;
        public Action<NodeConnectionPoint> OnClickConnectionPoint;
    
        protected override void ApplyStyles()
        {
            var inStyle = new GUIStyle();
            inStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inStyle.border = new RectOffset(4, 4, 12, 12);
            
            var outStyle = new GUIStyle();
            outStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outStyle.border = new RectOffset(4, 4, 12, 12);
            
            styles.Add(IN_CONNECTION_POINT_STYLE, inStyle);
            styles.Add(OUT_CONNECTION_POINT_STYLE, outStyle);
        }
        
        public NodeConnectionPoint(Node node, ConnectionPointType type,  Action<NodeConnectionPoint> OnClickConnectionPoint)
        {
            this.node = node;
            this.type = type;
            this.OnClickConnectionPoint = OnClickConnectionPoint;
            rect = new Rect(0, 0, 10f, 20f);
        }

        public abstract override void Draw(EmptyArgs args);
    }
}