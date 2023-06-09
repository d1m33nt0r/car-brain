using System.Collections.Generic;
using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common.Abstract;
using NeuralNet.Editor.Common.Drawers;
using NeuralNet.Editor.Common.Interfaces;
using NeuralNet.Editor.Template.LeftSection.Foldouts;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection
{
    public class SidebarDrawer : StylizedDrawer<EmptyArgs>, ISizeListener, IInitializable<SidebarInitArgs>
    {
        private const string AREA_STYLE = "area";

        private List<IDrawer<EmptyArgs>> foldouts = new();
        private List<bool> foldoutStates = new();
        private List<FoldoutButton> sidebarButtons = new();
        private float buttonHeight = 50;
        private Vector2 scrollPos;

        private VerticalSplitLine verticalSplitLine;
        
        private enum Tab
        {
            File,
            Randomize,
            ImportExport,
            BrainCollections
        }

        private string[] tabNames = { "File", "Randomization", "Import / Export", "Brain Collections" };

        public void Initialize(SidebarInitArgs args)
        {
            verticalSplitLine = args.VerticalSplitLine;
            verticalSplitLine.OnDragSplitLine += OnChangedVerticalSplitLine;
            args.State.onChangeNetworkAsset += SetCurrentAssetToFileFoldout;
            sidebarButtons.Clear();
            var initFoldoutState = false;
            for (var i = 0; i < tabNames.Length; i++)
            {
                var buttonInstance = new FoldoutButton();
                buttonInstance.Initialize(tabNames[i], i, initFoldoutState);
                buttonInstance.onClick += (index, state) => foldoutStates[index] = state;
                sidebarButtons.Add(buttonInstance);
                foldoutStates.Add(initFoldoutState);
            }

            var fileFoldout = new FileFoldout();
            fileFoldout.onChangeNetworkAsset += args.State.SetCurrentNetworkAsset;
            //fileFoldout.on
            foldouts.Add(fileFoldout);
            foldouts.Add(new RandomizeFoldout());
            foldouts.Add(new ImportExportFoldout());
            foldouts.Add(new RandomizeFoldout());
        }
        
        protected override void ApplyStyles()
        {
            styles.Add(AREA_STYLE, new GUIStyle());
            styles[AREA_STYLE].border = new RectOffset(10, 10, 10, 10);
            styles[AREA_STYLE].margin = new RectOffset(10, 10, 10, 10);
        }

        public override void Draw(EmptyArgs args)
        {
            GUILayout.BeginArea(rect, styles[AREA_STYLE]);
            
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(rect.width), GUILayout.Height(rect.height));
            
            for (var i = 0; i < sidebarButtons.Count; i++)
            {
                sidebarButtons[i].Draw(args);
                if (foldoutStates[i]) foldouts[i].Draw(args);
            }
            
            EditorGUILayout.EndScrollView();

            GUILayout.EndArea();
        }

        public void OnChangedRectSize(Vector2 newSize)
        {
            rect = new Rect(rect.position, new Vector2(verticalSplitLine.Width, newSize.y));
        }

        private void OnChangedVerticalSplitLine(float delta)
        {
            rect = new Rect(rect.position, new Vector2(delta, rect.size.y));
        }
        
        private void SetCurrentAssetToFileFoldout(NeuralNetworkData brainAsset)
        {
            (foldouts[0] as FileFoldout).SetCurrentNetworkAsset(brainAsset, false);
        }
    }
}