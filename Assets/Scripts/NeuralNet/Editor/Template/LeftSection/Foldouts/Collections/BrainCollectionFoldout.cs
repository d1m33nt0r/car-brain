using System;
using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Args.StyleArgs;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Interfaces;
using NeuralNet.Editor.Template.LeftSection.Foldouts.CollectionFoldout;
using NeuralNet.Editor.Template.LeftSection.Foldouts.Collections;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class BrainCollectionFoldout : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<EmptyArgs>
    {
        public event Action OnCollectionAdded; 

        private List<BrainCollectionView> collectionViews;
        private List<BrainCollection> collections;
        private CollectionCreationFoldout creationFoldout;
        private bool foldoutState;

        private BrainCollectionViewStyleArgs[] args = new []{new BrainCollectionViewStyleArgs(new Color(0.22f, 0.22f, 0.22f, 1f)), new BrainCollectionViewStyleArgs(new Color(0.21f, 0.21f, 0.21f, 1f))};
        
        protected override void DrawInternal(EmptyArgs args)
        {
            for (var i = 0; i < collectionViews.Count; i++)
                collectionViews[i].Draw(Constants.Args.EmptyArgs);

            if (GUILayout.Button("Create collection")) foldoutState = !foldoutState;
            if (foldoutState) creationFoldout.Draw(Constants.Args.EmptyArgs);
        }

        public BrainCollectionFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
            
        }
        
        public void Initialize(EmptyArgs args)
        {
            collectionViews = new List<BrainCollectionView>();
            creationFoldout = new (Constants.Args.EmptyArgs);
            creationFoldout.OnCollectionAdded += () =>
            {
                Initialize(Constants.Args.EmptyArgs);
                OnCollectionAdded?.Invoke();
            };
            collections = BrainEditorWindow.Instance.State.EditorData.collections;
            for (var i = 0; i < collections.Count; i++)
            {
                var s = i % 2;
                var collection = new BrainCollectionView(this.args[s]);
                collectionViews.Add(collection);
                collection.Initialize(new BrainCollectionInitArgs(collections[i]));
            }
        }
    }
}