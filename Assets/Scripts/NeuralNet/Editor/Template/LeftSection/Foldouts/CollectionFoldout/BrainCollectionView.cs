using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts.Collections
{
    public class BrainCollectionView : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<BrainCollectionInitArgs>
    {
        private bool isShow = true;
        private List<BrainCollectionItemView> items;
        private string collectionName;
        
        protected override void ApplyStyles(EmptyArgs args)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.25f, 0.25f, 0.25f, 1f));
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.normal.background = canvasBackground;
        }

        public override void Draw(EmptyArgs args)
        {
            isShow = EditorGUILayout.Foldout(isShow, collectionName);
            if (isShow) base.Draw(args);
        }

        protected override void Draw()
        {
            if (items == null) return;
            for (var i = 0; i < items.Count; i++)
            {
                items[i].Draw(Constants.Args.EmptyArgs);
            }
        }

        public BrainCollectionView(EmptyArgs styleArgs) : base(styleArgs)
        {
        }

        public void Initialize(BrainCollectionInitArgs args)
        {
            items = new List<BrainCollectionItemView>();
            collectionName = args.brainCollection.name;
            for (var i = 0; i < args.brainCollection.data.Count; i++)
            {
                var itemView = new BrainCollectionItemView(Constants.Args.EmptyArgs);
                items.Add(itemView);
                itemView.Initialize(new BrainViewInitArgs(args.brainCollection.data[i]));
            }
        }
    }
}