using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeuralNet.Core;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Args.Init;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Drawers;
using NeuralNet.Editor.Common.Interfaces;
using NeuralNet.Editor.Template.BottomSection;
using NeuralNet.Editor.Template.LeftSection;
using UnityEditor;
using UnityEngine;
using Canvas = NeuralNet.Editor.Template.MiddleSection.Canvas;


namespace NeuralNet.Editor
{
    public class BrainEditorWindow : EditorWindowSingleton<BrainEditorWindow>
    {
        protected override string windowTitle => "Brain Editor";
        public GlobalState State { get; private set; }

        private SidebarDrawer sidebarDrawer;
        private SidebarInitArgs sidebarInitArgs;

        private VerticalSplitLine verticalSplitLine;
        private HorizontalSplitLine horizontalSplitLine;
        private HorizontalLineInitArgs horizontalLineInitArgs;

        private Canvas canvas;
        private CanvasInitArgs canvasInitArgs;

        private TestView testView;
        private TestViewInitArgs testViewInitArgs;
        private TestViewDrawArgs testViewDrawArgs;

        private List<ISizeListener> sizeListeners;

        private Vector2 lastWinSize;

        [MenuItem("Window/Brain Editor")]
        private static void OpenWindow()
        {
            var window = Instance;
            window.minSize = Constants.Window.minSize;
        }

        private void OnEnable()
        {
            PrepareDependencies();
            Initialize();
        }

        private void Initialize()
        {
            var path = Constants.Window.editorDataFolderPath;
            var asset = AssetDatabase.LoadAssetAtPath<EditorData>($"{path}/EditorData.asset");
            var assetExists = asset != null;
            if (!assetExists)
            {
                var editorData = CreateInstance<EditorData>();
                editorData.name = "EditorData";
                var path2 = $"{path}/{editorData.name}.asset";
                AssetDatabase.CreateAsset(editorData, path2);
                AssetDatabase.SaveAssets();
                State.SetEditorData(editorData);
            }
            else
            {
                State.SetEditorData(asset);
            }
            sizeListeners.Add(sidebarDrawer);
            sizeListeners.Add(verticalSplitLine);
            sizeListeners.Add(canvas);
            sizeListeners.Add(horizontalSplitLine);
            sizeListeners.Add(testView);

            horizontalSplitLine.Initialize(horizontalLineInitArgs);
            sidebarDrawer.Initialize(sidebarInitArgs);
            canvas.Initialize(canvasInitArgs);
            testView.Initialize(testViewInitArgs);

            State.SetCurrentNetworkAsset(CreateInstance<BrainData>());
        }

        private void PrepareDependencies()
        {
            canvas = new();
            sidebarDrawer = new();
            verticalSplitLine = new();
            sizeListeners = new();
            State = new GlobalState();
            horizontalLineInitArgs = new HorizontalLineInitArgs(verticalSplitLine);
            canvasInitArgs = new CanvasInitArgs(verticalSplitLine, State);
            sidebarInitArgs = new SidebarInitArgs(verticalSplitLine, State);
            horizontalSplitLine = new HorizontalSplitLine();
            testView = new TestView();
            testViewDrawArgs = new TestViewDrawArgs(true);
            testViewInitArgs = new TestViewInitArgs(horizontalSplitLine, verticalSplitLine);
        }

        private void OnGUI()
        {
            sidebarDrawer.Draw(Constants.Args.EmptyArgs);
            canvas.Draw(Constants.Args.EmptyArgs);
            testView.Draw(testViewDrawArgs);

            if (lastWinSize != position.size)
            {
                lastWinSize = position.size;
                for (var i = 0; i < sizeListeners.Count; i++)
                    sizeListeners[i].OnChangedRectSize(lastWinSize);
            }

            verticalSplitLine.Draw(Constants.Args.EmptyArgs);
            horizontalSplitLine.Draw(Constants.Args.EmptyArgs);
            HorizontalLine(new Color(0.15f, 0.15f, 0.15f, 1f));
        }

        private void HorizontalLine(Color color)
        {
            var horizontalLine = new GUIStyle();
            horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
            horizontalLine.fixedHeight = 1;
            var rec2t = new Rect(new Vector2(0, 0), new Vector2(lastWinSize.x, 0));
            var c = GUI.color;
            GUI.color = color;
            GUILayout.BeginArea(rec2t, GUIContent.none, horizontalLine);
            GUILayout.EndArea();
            GUI.color = c;
        }
    }
}