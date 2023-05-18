using NeuralNet.Core;
using NeuralNet.Editor.Template;

namespace NeuralNet.Editor
{
    public class GlobalState
    {
        public BrainController brainController;
        public NeuralNetworkData CurrentNetworkAsset = new ();
        public bool showAssetPopup;
        public bool showTestPopup;
        public NewAssetData NewAssetData = new ();
    }
}