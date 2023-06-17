using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Template.LeftSection.Foldouts.CollectionFoldout;
using NeuralNet.Editor.Template.LeftSection.Foldouts.Collections;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class BrainCollectionFoldout : BaseFoldout<EmptyArgs, EmptyArgs>
    {
        private List<BrainCollectionView> collectionViews = new ();
        private List<BrainCollection> collections;
        private CollectionCreationFoldout creationFoldout = new (Constants.Args.EmptyArgs);
        private bool foldoutState;
        
        protected override void Draw()
        {
            for (var i = 0; i < collectionViews.Count; i++)
                collectionViews[i].Draw(Constants.Args.EmptyArgs);

            if (GUILayout.Button("Create collection")) foldoutState = !foldoutState;
            if (foldoutState) creationFoldout.Draw(Constants.Args.EmptyArgs);
        }

        public BrainCollectionFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
            collections = BrainEditorWindow.Instance.State.EditorData.collections;
            for (var i = 0; i < collections.Count; i++)
            {
                var collection = new BrainCollectionView(styleArgs);
                collectionViews.Add(collection);
                collection.Initialize(new BrainCollectionInitArgs(collections[i]));
            }
        }
    }
}