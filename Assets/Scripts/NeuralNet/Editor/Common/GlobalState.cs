using System;
using NeuralNet.Core;
using NeuralNet.Editor.Template;

namespace NeuralNet.Editor.Common
{
    public class GlobalState
    {
        public event Action<BrainData> onChangeNetworkAsset;
        
        public BrainData CurrentNetworkAsset { get; private set; }
        public bool showAssetPopup;
        public bool showTestPopup;
        public NewAssetData NewAssetData = new ();
        public EditorData EditorData { get; private set; }
        
        public void SetCurrentNetworkAsset(BrainData brainAsset)
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