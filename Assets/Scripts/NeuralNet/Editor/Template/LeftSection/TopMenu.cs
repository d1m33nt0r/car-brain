using System;
using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NeuralNet.Editor.Template.LeftSection
{
    public class TopMenu : StylizedDrawer<EmptyArgs>
    {
        public NewAssetPopup newAssetPopup = new ();
        public TestPopup testPopup = new();
        
        public event Action OnChangedCurrentNetworkAsset;
        private string[] menuItems = { "New", "Open", "Randomization", "Save as", "Show activate order" };
        protected override void ApplyStyles()
        {
            rect = new Rect(new Vector2(0, 0), new Vector2(BrainEditorWindow.Instance.position.width, 23));
        }

        public override void Draw(EmptyArgs args)
        {
            EditorGUI.DrawRect(rect, new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f));
            GUILayout.BeginHorizontal();

            var sideButtonRect = BrainEditorWindow.Instance.position.width / 6;
            GUILayout.BeginArea(new Rect(new Vector2(0,0), new Vector2(sideButtonRect, 23)));

            if (GUILayout.Button("New"))
            {
                BrainEditorWindow.Instance.State.showAssetPopup = !BrainEditorWindow.Instance.State.showAssetPopup;
            }
            GUILayout.EndArea();
            
            DrawNewAssetPopup(sideButtonRect);
            
            GUILayout.BeginArea(new Rect(new Vector2(sideButtonRect,0), new Vector2(sideButtonRect, 23)));
            if (GUILayout.Button("Randomize"))
            {
                var hiddenNeurons = BrainEditorWindow.Instance.State.CurrentNetworkAsset.hiddenNeurons;
                foreach (var neuron in hiddenNeurons)
                {
                    neuron.bias = Random.Range(-50f, 50f);
                    foreach (var weight in neuron.inputWeights)
                    {
                        weight.data = Random.Range(-30f, 30f);
                    }
                }
            }
            GUILayout.EndArea();
            
            GUILayout.BeginArea(new Rect(new Vector2(sideButtonRect * 2,0), new Vector2(sideButtonRect, 23)));
            /*if (GUILayout.Button("Open"))
            {
                var path = EditorUtility.OpenFilePanel("Choose neural network asset", "Assets", "json");
                if (path.Length > 0)
                {
                    var s = Serializer.ReadFromJson(path);
                    BrainEditorWindow.Instance.State.CurrentNetworkAsset = s;
                    BrainEditorWindow.Instance.State.BrainController = new BrainController(s);
                    OnChangedCurrentNetworkAsset?.Invoke();
                }
            }*/
            GUILayout.EndArea();
            
            GUILayout.BeginArea(new Rect(new Vector2(sideButtonRect * 3,0), new Vector2(sideButtonRect, 23)));
            if (GUILayout.Button("Save As"))
            {
                var s = EditorUtility.SaveFilePanel("Save network asset", "Assets", "BrainAsset", "json");
                if (s.Length > 0) Serializer.WriteToJson(s, BrainEditorWindow.Instance.State.CurrentNetworkAsset, true);
            }
            GUILayout.EndArea();
            
            GUILayout.BeginArea(new Rect(new Vector2(sideButtonRect * 4,0), new Vector2(sideButtonRect, 23)));
            if (GUILayout.Button("Test"))
            {
                BrainEditorWindow.Instance.State.showTestPopup = !BrainEditorWindow.Instance.State.showTestPopup;
            }
            GUILayout.EndArea();

            DrawTestPopup(new Vector2(sideButtonRect * 4, 23), new Vector2(sideButtonRect,
                ((BrainEditorWindow.Instance.State.CurrentNetworkAsset.inputNeurons.Count + 1) * 20) + 3));
            
            GUILayout.BeginArea(new Rect(new Vector2(sideButtonRect * 5,0), new Vector2(sideButtonRect, 23)));
            if (GUILayout.Button("Show activation order"))
            {
                Debug.Log("start");
                for (var i = 0; i < BrainEditorWindow.Instance.State.CurrentNetworkAsset.hiddenNeurons.Count; i++)
                {
                    Debug.Log($"neuron {BrainEditorWindow.Instance.State.CurrentNetworkAsset.hiddenNeurons[i].id}");
                }

                for (var i = 0; i < BrainEditorWindow.Instance.State.CurrentNetworkAsset.outputNeurons.Count; i++)
                {
                    Debug.Log($"neuron {BrainEditorWindow.Instance.State.CurrentNetworkAsset.outputNeurons[i].id}");
                }

                Debug.Log("end");
            }
            GUILayout.EndArea();

            GUILayout.EndHorizontal();
        }

        private void DrawNewAssetPopup(float width)
        {
            newAssetPopup.Draw(new NewAssetPopupDrawArgs(){width = width});
        }
        
        private void DrawTestPopup(Vector2 position, Vector2 size)
        {
            testPopup.Draw(new TestAssetPopupDrawArgs(){position = position, size = size});
        }
    }
}