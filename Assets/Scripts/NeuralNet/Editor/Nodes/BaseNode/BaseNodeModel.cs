using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Nodes.BaseNode
{
    public class BaseNodeModel : BaseModel
    {
        public Rect Rect;
        public string Title;
        public bool isDragged;

        public BaseNodeModel(Vector2 position, float width, float height, string title)
        {
            Rect = new Rect(position.x, position.y, width, height);
            Title = title;
        }
    }
}