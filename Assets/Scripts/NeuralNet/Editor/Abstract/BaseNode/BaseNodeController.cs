using System;
using NeuralNet.Editor.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Nodes.BaseNode
{
    public class BaseNodeController : BaseController<BaseNodeDrawer, BaseNodeModel>
    {
        public Action<BaseNodeController> OnRemoveNode;
        
        public void Drag(Vector2 delta)
        {
            model.Rect.position += delta;
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (model.Rect.Contains(e.mousePosition))
                        {
                            model.isDragged = true;
                            GUI.changed = true;
                            model.isSelected = true;
                            view.ChangeStyle(model.isSelected);
                        }
                        else
                        {
                            GUI.changed = true;
                            model.isSelected = false;
                            view.ChangeStyle(model.isSelected);
                        }
                    }
                    if (e.button == 1 && model.isSelected && model.Rect.Contains(e.mousePosition))
                    {
                        ProcessContextMenu();
                        e.Use();
                    }
                    break;
 
                case EventType.MouseUp:
                    model.isDragged = false;
                    break;
 
                case EventType.MouseDrag:
                    if (e.button == 0 && model.isDragged)
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
            var genericMenu = new GenericMenu();
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