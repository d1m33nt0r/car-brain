using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public class BaseConnectionModel : BaseModel
    {
        public BaseConnectionPointController inPointController;
        public BaseConnectionPointController outPointController;
        
        public BaseConnectionModel(BaseConnectionPointController inPointController, BaseConnectionPointController outPointController)
        {
            this.inPointController = inPointController;
            this.outPointController = outPointController;
        }
    }
}