using NeuralNet.Core;
using NeuralNet.Editor.Template;

namespace NeuralNet.Editor
{
    public class GlobalState
    {
        public NeuralNetworkData CurrentNetworkAsset = new ();
        public bool showAssetPopup;
        public NewAssetData NewAssetData = new ();
    }
}