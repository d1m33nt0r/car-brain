using NeuralNet.Core;
using NeuralNet.Editor.Args;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts.Collections
{
    public class BrainCollectionView : BaseFoldout<EmptyArgs, EmptyArgs>
    {
        private bool showPosition = true;
        private BrainData selectedObject;

        protected override void ApplyStyles(EmptyArgs args)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.25f, 0.25f, 0.25f, 1f));
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.normal.background = canvasBackground;
        }

        public override void Draw(EmptyArgs args)
        {
            showPosition = EditorGUILayout.Foldout(showPosition, "Test collection");
            if (showPosition)
            {
                base.Draw(args);
            }
        }

        protected override void Draw()
        {
            selectedObject = (BrainData) EditorGUILayout.ObjectField(selectedObject, typeof(BrainData));
        }

        public BrainCollectionView(EmptyArgs styleArgs) : base(styleArgs)
        {
        }
    }
}