using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public class BaseConnectionModel : BaseModel
    {
        public BaseConnectionPointModel inPoint;
        public BaseConnectionPointModel outPoint;
        
        public BaseConnectionModel(BaseConnectionPointModel inPoint, BaseConnectionPointModel outPoint)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
        }
    }
}