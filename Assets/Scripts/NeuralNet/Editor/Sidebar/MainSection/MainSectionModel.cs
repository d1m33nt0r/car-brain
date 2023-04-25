using NeuralNet.Core;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Sidebar.MainSection
{
    public class MainSectionModel : BaseModel
    {
        public NeuralNetworkData SelectedNetworkData;
        public string openFilePath;
        public string saveFilePath;
    }
}