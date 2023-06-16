using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Drawers;
using NeuralNet.Editor.Common.Interfaces;
using UnityEngine;

namespace NeuralNet.Editor.Template.BottomSection
{
    public class TestView : StylizedDrawer<EmptyArgs, TestViewDrawArgs>, ISizeListener, IInitializable<TestViewInitArgs>
    {
        private const string BACKGROUND_STYLE = "background";

        private HorizontalSplitLine horizontalSplitLine;
        private VerticalSplitLine verticalSplitLine;
        
        protected override void ApplyStyles(EmptyArgs args)
        {
            var background = new Texture2D(1, 1);
            background.SetPixel(1, 1, new Color(0.25f, 0.25f, 0.25f, 1f));
            background.Apply();
            var borderSize = 10;
            var style = new GUIStyle();
            style.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            style.normal.background = background;
            
            styles.Add(BACKGROUND_STYLE, style);
        }

        public override void Draw(TestViewDrawArgs args)
        {
            if (!args.testModeIsActive) return;

            var position = new Vector2(verticalSplitLine.Width + 1, rect.height - horizontalSplitLine.Height);
            var size = new Vector2(rect.width - verticalSplitLine.Width - 1, horizontalSplitLine.Height);
            var testViewArea = new Rect(position, size);
            GUILayout.BeginArea(testViewArea, styles[BACKGROUND_STYLE]);
            GUILayout.EndArea();
        }

        public void OnChangedRectSize(Vector2 newSize)
        {
            rect = new Rect(rect.position, newSize);
        }

        public void Initialize(TestViewInitArgs args)
        {
            horizontalSplitLine = args.horizontalSplitLine;
            verticalSplitLine = args.verticalSplitLine;

            horizontalSplitLine.OnDragSplitLine += f => rect = new Rect(new Vector2(rect.position.x, f), rect.size);
        }
    }
}