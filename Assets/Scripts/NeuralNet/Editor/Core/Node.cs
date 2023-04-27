using System;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class Node : StylizedDrawer<EmptyDrawerArgs>
    {
        public string title;
        public bool isDragged;
        
        public InConnectionPoint inPoint;
        public OutConnectionPoint outPoint;
        
        public Node(Vector2 position, float width, float height, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint)
        {
            rect = new Rect(position.x, position.y, width, height);
            inPoint = new InConnectionPoint(this, ConnectionPointType.In, OnClickInPoint);
            outPoint = new OutConnectionPoint(this, ConnectionPointType.Out, OnClickOutPoint);
        }
 
        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        protected override void ApplyStyles()
        {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            style.border = new RectOffset(12, 12, 12, 12);
        }

        public override void Draw(EmptyDrawerArgs args)
        {
            inPoint.Draw(args);
            outPoint.Draw(args);
            GUI.Box(rect, title, style);
        }
 
        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                        }
                        else
                        {
                            GUI.changed = true;
                        }
                    }
                    break;
 
                case EventType.MouseUp:
                    isDragged = false;
                    break;
 
                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}