using System;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection
{
    public class FoldoutButton : StylizedDrawer<EmptyArgs>
    {
        public event Action<int, bool> onClick;
        
        private const string DEFAULT_STYLE = "default";
        private const string HOVER_STYLE = "hover";

        private int buttonIndex;
        private string buttonText;
        private GUIStyle currentStyle = new();
        private bool isActive;

        public void Initialize(string buttonText, int buttonIndex, bool initState)
        {
            this.buttonText = buttonText;
            this.buttonIndex = buttonIndex;
            isActive = initState;
        }
        
        protected override void ApplyStyles()
        {
            var characterButton = new Texture2D(1, 1);
            characterButton.SetPixel(1, 1, new Color(0.17f, 0.17f, 0.17f, 0.8f));
            characterButton.Apply();
            var activeButton = new Texture2D(1, 1);
            activeButton.SetPixel(1, 1, new Color(0.75f, 0.4f, 0.78f, 0.1f));
            activeButton.Apply();
            var defaultStyle = new GUIStyle();
            defaultStyle.normal.background = characterButton;
            defaultStyle.active.background = activeButton;
            defaultStyle.alignment = TextAnchor.MiddleCenter;
            defaultStyle.normal.textColor = Color.white;
            defaultStyle.margin = new RectOffset(0, 0, 2, 0);

            var hoverButton = new Texture2D(1, 1);
            hoverButton.SetPixel(1, 1, new Color(0.2f, 0.4f, 0.78f, 0.1f));
            hoverButton.Apply();
            var hoverStyle = new GUIStyle();
            hoverStyle.normal.background = hoverButton;
            hoverStyle.active.background = activeButton;
            hoverStyle.alignment = TextAnchor.MiddleCenter;
            hoverStyle.normal.textColor = Color.white;
            hoverStyle.margin = new RectOffset(0, 0, 2, 0);
            
            styles.Add(DEFAULT_STYLE, defaultStyle);
            styles.Add(HOVER_STYLE, hoverStyle);
            currentStyle = styles[DEFAULT_STYLE];
        }

        public override void Draw(EmptyArgs args)
        {
            if (rect.Contains(Event.current.mousePosition)) currentStyle = styles[HOVER_STYLE];
            else currentStyle = styles[DEFAULT_STYLE];

            if (GUILayout.Button(buttonText, currentStyle, GUILayout.Height(34), GUILayout.ExpandWidth(true)))
            {
                isActive = !isActive;
                onClick?.Invoke(buttonIndex, isActive);
            }
        }
    }
}