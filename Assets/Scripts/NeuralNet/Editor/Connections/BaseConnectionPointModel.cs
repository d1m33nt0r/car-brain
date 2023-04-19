using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Nodes.BaseNode;
using UnityEngine;

namespace NeuralNet.Editor.Connections
{
    public abstract class BaseConnectionPointModel : BaseModel
    {
        public BaseNodeModel BaseNodeModel;
        public Rect Rect;
        public ConnectionPointType ConnectionPointType;

        public BaseConnectionPointModel(BaseNodeModel baseNodeModel, ConnectionPointType connectionPointType)
        {
            BaseNodeModel = baseNodeModel;
            ConnectionPointType = connectionPointType;
            Rect = new Rect(0, 0, Constants.General.DEFAULT_CONNECTION_POINT_SIZE.width, Constants.General.DEFAULT_CONNECTION_POINT_SIZE.height);
        }
    }
}