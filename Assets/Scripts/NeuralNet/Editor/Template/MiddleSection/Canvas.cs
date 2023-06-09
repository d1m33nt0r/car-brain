using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Drawers;
using NeuralNet.Editor.Common.Interfaces;
using UnityEngine;

namespace NeuralNet.Editor.Template.MiddleSection
{
    public class Canvas : StylizedDrawer<EmptyArgs>, ISizeListener, IInitializable<CanvasInitArgs>
    {
        private const string CANVAS_STYLE = "border";
        private const string TOOLBAR_STYLE = "toolbar";
        private const float TOOLBAR_HEIGHT = 24;
        
        private Graph graph;
        private GridDrawer gridDrawer;
        private VerticalSplitLine verticalSplitLine;

        private Toolbar toolbar;
            
        private float XCoord => verticalSplitLine.Width;
        private float lastXCoord;
        
        protected override void ApplyStyles()
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.25f, 0.25f, 0.25f, 1f));
            canvasBackground.Apply();
            var borderSize = 10;
            var canvasStyle = new GUIStyle();
            canvasStyle.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            canvasStyle.normal.background = canvasBackground;

            var toolbarBackground = new Texture2D(1, 1);
            toolbarBackground.SetPixel(1, 1, new Color(0.235f, 0.235f, 0.235f, 1f));
            toolbarBackground.Apply();
            var toolbarStyle = new GUIStyle();
            toolbarStyle.border = new RectOffset(10,10,10,10);
            toolbarStyle.normal.background = toolbarBackground;

            styles.Add(TOOLBAR_STYLE, toolbarStyle);
            styles.Add(CANVAS_STYLE, canvasStyle);
        }
        
        public void Initialize(CanvasInitArgs args)
        {
            verticalSplitLine = args.verticalSplitLine;
            gridDrawer = new GridDrawer();
            toolbar = new Toolbar();
            graph = new Graph();
            var graphInitArgs = new GraphInitArgs(args.State);
            var toolbarInitArgs = new ToolbarInitArgs(verticalSplitLine);
            graph.Initialize(graphInitArgs);
            toolbar.Initialize(toolbarInitArgs);
            lastXCoord = XCoord;
            OnChangedRectPosition();
        }

        public override void Draw(EmptyArgs args)
        {
            if (gridDrawer == null) return;
            if (lastXCoord != XCoord)
            {
                OnChangedRectPosition();
                lastXCoord = XCoord;
            }
            
            GUILayout.BeginArea(rect);
            
            GUILayout.BeginArea(new Rect(Vector2.zero, new Vector2(rect.width, TOOLBAR_HEIGHT)), styles[TOOLBAR_STYLE]);
            toolbar.Draw(Constants.Args.EmptyArgs);
            GUILayout.EndArea();
            
            GUILayout.BeginArea(new Rect(new Vector2(1, TOOLBAR_HEIGHT), new Vector2(rect.size.x, rect.size.y - TOOLBAR_HEIGHT)), styles[CANVAS_STYLE]);
            gridDrawer.Draw();
            graph.Draw(Constants.Args.EmptyArgs);
            GUILayout.EndArea();
            
            GUILayout.EndArea();
        }

        private void OnChangedRectPosition()
        {
            var deltaX = XCoord - lastXCoord;
            var position = new Vector2(XCoord, rect.position.y);
            var size = new Vector2(rect.size.x - deltaX, rect.size.y);
            rect = new Rect(position, size);
            gridDrawer.OnChangedRect(rect);
        }
        
        public void OnChangedRectSize(Vector2 newSize)
        {
            rect = new Rect(rect.position, new Vector2(newSize.x - XCoord, newSize.y));
            toolbar.OnChangedRectSize(newSize);
            gridDrawer.OnChangedRect(rect);
        }
    }
}