using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts.Collections
{
    public class BrainCollectionItemView : StylizedDrawer<EmptyArgs, EmptyArgs>, IInitializable<BrainViewInitArgs>
    {
        private Texture2D editButtonTexture;
        private Texture2D trashButtonTexture;
        private BrainData brainAsset;
        
        protected override void ApplyStyles(EmptyArgs args)
        {
            var path = "Assets/Scripts/NeuralNet/Editor/Resources/edit_icon.png";
            editButtonTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            path = "Assets/Scripts/NeuralNet/Editor/Resources/trash_icon.png";
            trashButtonTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        public override void Draw(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            
            EditorGUILayout.ObjectField(brainAsset, typeof(BrainData), GUILayout.Height(25));
            GUILayout.Button(editButtonTexture, GUILayout.Width(24), GUILayout.Height(25));
            GUILayout.Button(trashButtonTexture, GUILayout.Width(24), GUILayout.Height(25));
            
            GUILayout.EndHorizontal();
        }

        public BrainCollectionItemView(EmptyArgs styleArgs) : base(styleArgs)
        {
        }

        public void Initialize(BrainViewInitArgs args)
        {
            brainAsset = args.brainAsset;
        }
    }
}