using System;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class Node : StylizedDrawer<EmptyDrawerArgs>
    {
        public string title;
        public bool isDragged;
        public bool isSelected;
        
        public InConnectionPoint inPoint;
        public OutConnectionPoint outPoint;
        
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;
        
        public Action<Node> OnRemoveNode;

        public Node(Vector2 position, 
            Action<ConnectionPoint> OnClickInPoint, 
            Action<ConnectionPoint> OnClickOutPoint, 
            Action<Node> OnClickRemoveNode)
        {
            rect = new Rect(position.x, position.y, 50, 50);
            inPoint = new InConnectionPoint(this, ConnectionPointType.In, OnClickInPoint);
            outPoint = new OutConnectionPoint(this, ConnectionPointType.Out, OnClickOutPoint);
            OnRemoveNode = OnClickRemoveNode;
        }
 
        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        protected override void ApplyStyles()
        {
            defaultNodeStyle = new GUIStyle();
            defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);
            
            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            style = defaultNodeStyle;
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
                            isSelected = true;
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            GUI.changed = true;
                            isSelected = false;
                            style = defaultNodeStyle;
                        }
                    }
                    if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
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
        
        private void ProcessContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }
 
        private void OnClickRemoveNode()
        {
            if (OnRemoveNode != null)
            {
                OnRemoveNode(this);
            }
        }
    }
}