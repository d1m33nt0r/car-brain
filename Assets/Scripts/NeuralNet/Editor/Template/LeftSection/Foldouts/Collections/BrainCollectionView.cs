using System.Collections.Generic;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Args.StyleArgs;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts.Collections
{
    public class BrainCollectionView : BaseFoldout<BrainCollectionViewStyleArgs, EmptyArgs>, IInitializable<BrainCollectionInitArgs>
    {
        private bool isShow;
        private List<BrainCollectionItemView> items;
        private string collectionName;
        
        protected override void ApplyStyles(BrainCollectionViewStyleArgs args)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, args.foldoutColor);
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.margin = new RectOffset(borderSize,borderSize,borderSize,borderSize);
            foldoutStyle.normal.background = canvasBackground;
        }
        
        protected override void DrawInternal(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            isShow = EditorGUILayout.Foldout(isShow, collectionName);
            GUILayout.Button("delete", GUILayout.Height(16));
            GUILayout.EndHorizontal();
            if (!isShow) return;
            if (items == null) return;
            for (var i = 0; i < items.Count; i++)
            {
                items[i].Draw(Constants.Args.EmptyArgs);
            }
        }

        public BrainCollectionView(BrainCollectionViewStyleArgs styleArgs) : base(styleArgs)
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