using System;
using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class FileFoldout : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<EmptyArgs>
    {
        public event Action OnBrainAdded; 
        public event Action<BrainData> onChangeNetworkAsset;
        public bool newAssetFoldoutState { get; private set; }
        private BrainData prevObject;
        private BrainData selectedObject;
        private NewAssetFoldout newAssetFoldout;
        private ImportExportBlock importExportBlock;
        
        protected override void DrawInternal(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Selected Brain");
            selectedObject = (BrainData) EditorGUILayout.ObjectField(selectedObject, typeof(BrainData));
            GUILayout.EndHorizontal();
            if (selectedObject != prevObject) SetCurrentNetworkAsset(selectedObject, true);
            var newAssetButton = GUILayout.Button("New asset");
            if (newAssetButton) newAssetFoldoutState = !newAssetFoldoutState;
            if (newAssetFoldoutState) newAssetFoldout.Draw(Constants.Args.EmptyArgs);
            if (GUILayout.Button("Save asset"))
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            importExportBlock.Draw(Constants.Args.EmptyArgs);
            prevObject = selectedObject;
        }

        public void SetCurrentNetworkAsset(BrainData brainAsset, bool onEvent)
        {
            selectedObject = brainAsset;
            if (!onEvent) return;
            onChangeNetworkAsset?.Invoke(brainAsset);
        }

        public FileFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
        }
        
        public void Initialize(EmptyArgs args)
        {
            importExportBlock = new ImportExportBlock(args);
            newAssetFoldout = new NewAssetFoldout(args);
            newAssetFoldout.OnBrainAdded += () => OnBrainAdded?.Invoke();
            newAssetFoldout.Initialize(new NewAssetFoldoutInitArgs(this));
        }
    }
}