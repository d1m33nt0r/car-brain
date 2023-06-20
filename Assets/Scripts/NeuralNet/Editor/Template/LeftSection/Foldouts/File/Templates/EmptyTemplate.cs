using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class EmptyTemplate : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<EmptyTemplateInitArgs>
    {
        public NewAssetFoldout parent;
        
        public EmptyTemplate(EmptyArgs styleArgs) : base(styleArgs)
        {
        }

        protected override void ApplyStyles(EmptyArgs args)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.21f, 0.21f, 0.21f, 1f));
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.margin = new RectOffset(0, 0, 0, 4);
            foldoutStyle.normal.background = canvasBackground;
        }

        protected override void DrawInternal(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Asset name");
            parent.assetName = EditorGUILayout.TextField(parent.assetName);
            GUILayout.EndHorizontal();
                
            GUILayout.BeginHorizontal();
            GUILayout.Label("Collection");
            parent.currentCollectionIndex = EditorGUILayout.Popup(parent.currentCollectionIndex, parent.collectionNames);
            GUILayout.EndHorizontal();
                
            GUILayout.BeginHorizontal();
            GUILayout.Label("Template");
            parent.currentTemplateIndex = EditorGUILayout.Popup(parent.currentTemplateIndex, parent.templateNames);
            GUILayout.EndHorizontal();
        }

        public void Initialize(EmptyTemplateInitArgs args)
        {
            parent = args.newAssetFoldout;
        }
    }
}