using NeuralNet.Core;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Sidebar.MainSection
{
    public class MainSectionModel : BaseModel
    {
        public NeuralNetworkModel selectedNetworkModel;
        public string openFilePath;
        public string saveFilePath;
    }
}