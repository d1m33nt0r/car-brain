using System;
using NeuralNet.Core;
using NeuralNet.Editor.Args;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class FileFoldout : BaseFoldout<EmptyArgs, EmptyArgs>
    {
        public event Action<BrainData> onChangeNetworkAsset;
        public event Action<string> onChangeAssetPath;
        
        private BrainData prevObject;
        
        private BrainData selectedObject;
        private string assetPath;
        
        private bool newAssetFoldoutState;
        private string assetName;

        protected override void Draw()
        {
            GUILayout.Label("Brain Asset:");
            selectedObject = (BrainData) EditorGUILayout.ObjectField(selectedObject, typeof(BrainData));
            if (selectedObject != prevObject) SetCurrentNetworkAsset(selectedObject, true);

            var newAssetButton = GUILayout.Button("New asset");
            if (newAssetButton) newAssetFoldoutState = !newAssetFoldoutState;
            if (newAssetFoldoutState)
            {
                
                GUILayout.BeginHorizontal();
                var prefix = "Assets";
                GUILayout.Label("Asset folder");
                if (!string.IsNullOrEmpty(assetPath))
                {
                    string newPath;
                    if (assetPath.StartsWith(prefix)) newPath = assetPath;
                    else newPath = prefix + assetPath.Substring(assetPath.IndexOf(prefix, StringComparison.Ordinal) + prefix.Length);
                    GUILayout.Label(newPath);
                }
                GUILayout.EndHorizontal();
                
                if (GUILayout.Button("Change"))
                {
                    assetPath = EditorUtility.OpenFolderPanel("Asset folder", "", "");
                }
                
                GUILayout.Label("Asset name");
                GUILayout.BeginHorizontal();
                assetName = EditorGUILayout.TextField(assetName);
                if (GUILayout.Button("Create"))
                {
                    var brainAsset = ScriptableObject.CreateInstance<BrainData>();
                    brainAsset.brainName = assetName;
                    var path = $"Assets/Plugins/EasyBrain/Editor/Data/{assetName}.asset";
                    AssetDatabase.CreateAsset(brainAsset, path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    SetCurrentNetworkAsset(brainAsset, true);
                }
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Save asset"))
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

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
    }
}