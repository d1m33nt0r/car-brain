using System;
using NeuralNet.Core;
using NeuralNet.Editor.Template;

namespace NeuralNet.Editor.Common
{
    public class GlobalState
    {
        public event Action<NeuralNetworkData> onChangeNetworkAsset;
        
        public NeuralNetworkData CurrentNetworkAsset { get; private set; }
        public bool showAssetPopup;
        public bool showTestPopup;
        public NewAssetData NewAssetData = new ();
        public EditorData EditorData { get; private set; }
        
        public void SetCurrentNetworkAsset(NeuralNetworkData brainAsset)
        {
            CurrentNetworkAsset = brainAsset;
            onChangeNetworkAsset?.Invoke(brainAsset);
        }

        public void SetEditorData(EditorData editorData)
        {
            EditorData = editorData;
        }
    }
}