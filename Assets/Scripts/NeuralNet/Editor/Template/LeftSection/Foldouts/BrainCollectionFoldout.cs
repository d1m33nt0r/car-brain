using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Template.LeftSection.Foldouts.Collections;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class BrainCollectionFoldout : BaseFoldout<EmptyArgs, EmptyArgs>
    {
        private List<BrainCollectionView> collectionViews = new ();

        protected override void Draw()
        {
            for (var i = 0; i < collectionViews.Count; i++)
                collectionViews[i].Draw(Constants.Args.EmptyArgs);

            if (GUILayout.Button("Create collection"))
            {
                
            }
        }

        public BrainCollectionFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
            for (var i = 0; i < 9; i++)
            {
                collectionViews.Add(new BrainCollectionView(styleArgs));
            }
        }
    }
}