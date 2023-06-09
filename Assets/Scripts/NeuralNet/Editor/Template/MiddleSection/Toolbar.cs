using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Drawers;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.MiddleSection
{
    public class Toolbar : IDrawer<EmptyArgs>, ISizeListener, IInitializable<ToolbarInitArgs>
    {
        private const float TOOLBAR_WIDTH = 300;
        private const float SETTINGS_BUTTON_WITDH = 30;
        private const float SETTINGS_BUTTON_MARGIN = 2;
        
        private Vector2 windowSize;
        private VerticalSplitLine verticalSplitLine;
        private Texture2D settingsButtonIcon;
        private Texture2D leftArrowButtonIcon;
        private Texture2D rightArrowButtonIcon;
        
        public enum Tab
        {
            EditMode,
            TestMode,
            LearningMode
        }
        
        private string[] toolbarStrings =
        {
            "Edit mode",
            "Test mode",
            "Learning mode"
        };
        
        public Tab current = Tab.EditMode;

        public Tab Current
        {
            get { return current; }
            set { current = value; }
        }
        
        public void Draw(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            var toolbarSectionWidth = windowSize.x - verticalSplitLine.Width;
            var center = toolbarSectionWidth / 2 - TOOLBAR_WIDTH / 2;
            GUILayout.Space(center);
            current = (Tab) GUILayout.Toolbar((int) current, toolbarStrings,GUILayout.Width(TOOLBAR_WIDTH));
            
            GUI.Button(new Rect(new Vector2(toolbarSectionWidth - SETTINGS_BUTTON_WITDH - SETTINGS_BUTTON_MARGIN, 2), new Vector2(28, 20)), settingsButtonIcon);

            DrawSidebarArrowButton();

            GUILayout.EndHorizontal();
        }

        private void DrawSidebarArrowButton()
        {
            var texture = verticalSplitLine.isHidden ? rightArrowButtonIcon : leftArrowButtonIcon;
            var splitLinePos = verticalSplitLine.isHidden ? 0 : -Constants.LeftSection.MIN_WIDTH;
            var btnRect = new Rect(new Vector2(SETTINGS_BUTTON_MARGIN, 2), new Vector2(20, 20));
            if (GUI.Button(btnRect, texture)) verticalSplitLine.SetPosition(splitLinePos);
        }

        public void OnChangedRectSize(Vector2 newSize)
        {
            windowSize = newSize;
        }

        public void Initialize(ToolbarInitArgs args)
        {
            verticalSplitLine = args.verticalSplitLine;
            var path = "Assets/Scripts/NeuralNet/Editor/Resources/settings_icon.png";
            settingsButtonIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            path = "Assets/Scripts/NeuralNet/Editor/Resources/left_arrow_icon.png";
            leftArrowButtonIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            path = "Assets/Scripts/NeuralNet/Editor/Resources/right_arrow_icon.png";
            rightArrowButtonIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }
    }
}