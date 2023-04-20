using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Nodes.BaseNode
{
    public class BaseNodeController : BaseController<BaseNodeDrawer, BaseNodeModel>
    {
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
                        }
                        else
                        {
                            GUI.changed = true;
                        }
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
    }
}