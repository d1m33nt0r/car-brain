using System;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Common.Drawers
{
    public class HorizontalSplitLine : StylizedDrawer<EmptyArgs, EmptyArgs>, ISizeListener, IInitializable<HorizontalLineInitArgs>
    {
        public event Action<float> OnDragSplitLine;

        private const string HORIZONTAL_LINE = "horizontal";

        public bool isHidden => Height == 0;
        
        public float Height => Constants.BottomSection.MIN_HEIGHT + height + mouseDelta;
        private float height;
        private float mouseDelta;
        
        private Rect rec2t;
        private bool isDragged;
        private Vector2 startDragPoint;
        private VerticalSplitLine splitLine;
        
        protected override void ApplyStyles(EmptyArgs args)
        {
            var lineStyle = new GUIStyle();
            lineStyle.normal.background = EditorGUIUtility.whiteTexture;
            lineStyle.fixedHeight = 1f;
            styles.Add(HORIZONTAL_LINE, lineStyle);
        }

        public override void Draw(EmptyArgs args)
        {
            HorizontalLine(new Color(0.15f, 0.15f, 0.15f, 1f));
            EditorGUIUtility.AddCursorRect(rec2t, MouseCursor.SplitResizeUpDown);
            ProcessEvents();
        }
        
        private void HorizontalLine ( Color color )
        {
            var diffence = rect.height - Height;
            rec2t = new Rect(new Vector2(splitLine.Width,  diffence), new Vector2(rect.width - splitLine.Width, 5));
            var c = GUI.color;
            GUI.color = color;
            GUILayout.BeginArea(rec2t, GUIContent.none, styles[HORIZONTAL_LINE]);
            GUILayout.EndArea();
            GUI.color = c;
        }
        
        private void ProcessEvents()
        {
            if (!rec2t.Contains(Event.current.mousePosition) && !isDragged) return;
            
            switch (Event.current.type)
            {
                case EventType.MouseDown:
                    if (Event.current.button == 0)
                    {
                        isDragged = true;
                        startDragPoint = Event.current.mousePosition;
                        mouseDelta = 0;
                    }
                    break;
                case EventType.MouseUp:
                    isDragged = false;
                    height += mouseDelta;
                    mouseDelta = 0;
                    break;
 
                case EventType.MouseDrag:
                    if (Event.current.button == 0 && isDragged)
                    {
                        Drag(startDragPoint - Event.current.mousePosition);
                        Event.current.Use();
                    }
                    break;
            }
        }

        private void Drag(Vector2 delta)
        {
            mouseDelta = delta.y;
            OnDragSplitLine?.Invoke(Height);
        }

        public void SetPosition(float height)
        {
            this.height = height;
            OnDragSplitLine?.Invoke(Height);
        }
        
        public void OnChangedRectSize(Vector2 newSize)
        {
            rect = new Rect(rect.position, new Vector2(newSize.x, newSize.y));
        }

        public void Initialize(HorizontalLineInitArgs args)
        {
            splitLine = args.splitLine;
        }
    }
}