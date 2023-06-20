using System;
using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class NewAssetFoldout : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<NewAssetFoldoutInitArgs>
    {
        public event Action OnBrainAdded;

        private string assetPath;
        public string assetName;
        private FileFoldout fileFoldout;
        
        public string[] collectionNames;
        public int currentCollectionIndex;
        
        public string[] templateNames;
        public int currentTemplateIndex;
        private FullPerceptronTemplate fullPerceptronTemplate;
        private EmptyTemplate emptyTemplate;
        
        public NewAssetFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
        }

        protected override void DrawInternal(EmptyArgs args)
        {
            if (fileFoldout.newAssetFoldoutState)
            {
                emptyTemplate.Draw(args);
                
                if (currentTemplateIndex == 1) fullPerceptronTemplate.Draw(Constants.Args.EmptyArgs);
                
                if (GUILayout.Button("Create"))
                {
                    var brainAsset = ScriptableObject.CreateInstance<BrainData>();
                    var collectionAsset = AssetDatabase.LoadAssetAtPath<BrainCollection>($"Assets/Plugins/EasyBrain/Editor/Data/Collections/{collectionNames[currentCollectionIndex]}/{collectionNames[currentCollectionIndex]}.asset");
                    brainAsset.brainName = assetName;
                    var path = $"Assets/Plugins/EasyBrain/Editor/Data/Collections/{collectionNames[currentCollectionIndex]}/{assetName}.asset";
                    AssetDatabase.CreateAsset(brainAsset, path);
                    collectionAsset.data.Add(brainAsset);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    fileFoldout.SetCurrentNetworkAsset(brainAsset, true);
                    OnBrainAdded?.Invoke();
                }
            }
        }

        public void Initialize(NewAssetFoldoutInitArgs args)
        {
            fileFoldout = args.fileFoldout;
            var collections = BrainEditorWindow.Instance.State.EditorData.collections;
            collectionNames = new string[collections.Count];
            for (var i = 0; i < collections.Count; i++) collectionNames[i] = collections[i].name;
            templateNames = Enum.GetNames(typeof(BrainTemplate));
            fullPerceptronTemplate = new FullPerceptronTemplate(Constants.Args.EmptyArgs);
            fullPerceptronTemplate.Initialize(Constants.Args.EmptyArgs);
            emptyTemplate = new EmptyTemplate(Constants.Args.EmptyArgs);
            emptyTemplate.Initialize(new EmptyTemplateInitArgs(this));
        }
    }
}