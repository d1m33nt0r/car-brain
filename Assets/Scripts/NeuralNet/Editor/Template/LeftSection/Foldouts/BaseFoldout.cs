using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public abstract class BaseFoldout<TStyleArgs, TDrawArgs> : StylizedDrawer<TStyleArgs, TDrawArgs> where TStyleArgs : BaseArgs where TDrawArgs : BaseArgs
    {
        protected GUIStyle foldoutStyle;
        
        protected override void ApplyStyles(TStyleArgs args)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.2f, 0.25f, 0.25f, 1f));
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.normal.background = canvasBackground;
        }

        public override void Draw(TDrawArgs args)
        {
            GUILayout.BeginVertical(foldoutStyle);
            
            Draw();            
            
            GUILayout.EndVertical();
        }

        protected abstract void Draw();

        protected BaseFoldout(TStyleArgs styleArgs) : base(styleArgs)
        {
        }
    }
}