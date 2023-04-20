using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Connections;
using UnityEngine;

namespace NeuralNet.Editor.Nodes.BaseNode
{
    public class BaseNodeModel : BaseModel
    {
        public Rect Rect;
        public string Title;
        public bool isDragged;
        public BaseConnectionPointDrawer inPointDrawer;
        public BaseConnectionPointDrawer outPointDrawer;
        public BaseConnectionPointModel inPointModel;
        public BaseConnectionPointModel outPointModel;
        public BaseConnectionPointController inPointController;
        public BaseConnectionPointController outPointController;
        
        public BaseNodeModel(Vector2 position, float width, float height, string title)
        {
            Rect = new Rect(position.x, position.y, width, height);
            Title = title;
            inPointDrawer = new BaseInConnectionPointDrawer();
            outPointDrawer = new BaseOutConnectionPointDrawer();
            inPointModel = new BaseConnectionPointModel(this, ConnectionPointType.In);
            outPointModel = new BaseConnectionPointModel(this, ConnectionPointType.Out);
            inPointController = new BaseConnectionPointController();
            inPointController.AttachDrawer(inPointDrawer);
            inPointController.AttachModel(inPointModel);
            outPointController = new BaseConnectionPointController();
            outPointController.AttachDrawer(outPointDrawer);
            outPointController.AttachModel(outPointModel);
        }
    }
}