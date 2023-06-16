/*using System.Linq;
using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template
{
    public class TestPopup : StylizedDrawer<TestAssetPopupDrawArgs>
    {
        private float height;

        protected override void ApplyStyles()
        {
        }

        public override void Draw(TestAssetPopupDrawArgs drawArgs)
        {
            if (!BrainEditorWindow.Instance.State.showTestPopup) return;

            var data = BrainEditorWindow.Instance.State.CurrentNetworkAsset;
            var customRect = new Rect(drawArgs.position, drawArgs.size + new Vector2(0, 40));
            EditorGUI.DrawRect(customRect, new Color(0.2196079f, 0.42f, 0.2196079f, 1f));

            GUILayout.BeginArea(customRect, new GUIStyle { contentOffset = new Vector2(15, 15) });
            
            GUILayout.BeginVertical();

            for (var i = 0; i < data.inputNeurons.Count; i++)
            {
                GUILayout.BeginHorizontal();
                data.inputNeurons[i].data = EditorGUILayout.DoubleField(data.inputNeurons[i].data);
                GUILayout.EndHorizontal();
            }

            countIterations = EditorGUILayout.IntField("Iterations", countIterations);
            
            if (GUILayout.Button("Feed Forward"))
            {
                //var inputs = data.inputNeurons.Select(s => s.data);
                //var output = BrainEditorWindow.Instance.State.BrainController.FeedForward(inputs.ToArray());
                //for (var i = 0; i < output.Length; i++) Debug.Log($"output[{i}] = {output[i]}");
            }

            if (GUILayout.Button("Back Propagate"))
            {
                //var inputs = data.inputNeurons.Select(s => s.data);
                //for (var i = 0; i < countIterations; i++)
                //{
                    //BrainEditorWindow.Instance.State.BrainController.BackPropagation(inputs.ToArray(), new double[]{1.25f, 0.5f});
                //}
            }
            
            GUILayout.EndVertical();

            GUILayout.EndArea();
        }

        private int countIterations;
    }
}*/