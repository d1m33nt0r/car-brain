using System;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Common.Drawers
{
    public class VerticalSplitLine : StylizedDrawer<EmptyArgs, EmptyArgs>, ISizeListener
    {
        public event Action<float> OnDragSplitLine;

        private const string VERTICAL_LINE = "vertical";

        public bool isHidden => Width == 0;
        
        public float Width => Constants.LeftSection.MIN_WIDTH + width + mouseDelta;
        private float width;
        private float mouseDelta;
        
        private Rect rec2t;
        private bool isDragged;
        private Vector2 startDragPoint;

        protected override void ApplyStyles(EmptyArgs args)
        {
            var verticalLine = new GUIStyle();
            verticalLine.normal.background = EditorGUIUtility.whiteTexture;
            verticalLine.fixedWidth = 1f;
            styles.Add(VERTICAL_LINE, verticalLine);
        }
        private void VerticalLine ( Color color )
        {
            rec2t = new Rect(new Vector2(Width, 0), new Vector2(5, rect.height));
            var c = GUI.color;
            GUI.color = color;
            GUILayout.BeginArea(rec2t, GUIContent.none, styles[VERTICAL_LINE]);
            GUILayout.EndArea();
            GUI.color = c;
        }

        public override void Draw(EmptyArgs args)
        {
            VerticalLine(new Color(0.15f, 0.15f, 0.15f, 1f));
            EditorGUIUtility.AddCursorRect(rec2t, MouseCursor.SplitResizeLeftRight);
            ProcessEvents();
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
                    width += mouseDelta;
                    mouseDelta = 0;
                    break;
 
                case EventType.MouseDrag:
                    if (Event.current.button == 0 && isDragged)
                    {
                        Drag(Event.current.mousePosition - startDragPoint);
                        Event.current.Use();
                    }
                    break;
            }
        }

        private void Drag(Vector2 delta)
        {
            mouseDelta = delta.x;
            OnDragSplitLine?.Invoke(Width);
        }

        public void SetPosition(float width)
        {
            this.width = width;
            OnDragSplitLine?.Invoke(Width);
        }
        
        public void OnChangedRectSize(Vector2 newSize)
        {
            rect = new Rect(rect.position, new Vector2(rect.size.x, newSize.y));
        }
    }
}