using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template
{
    public class TestPopup : StylizedDrawer<TestAssetPopupArgs>
    {
        private float height;

        protected override void ApplyStyles()
        {
        }

        public override void Draw(TestAssetPopupArgs args)
        {
            if (!BrainEditorWindow.Instance.State.showTestPopup) return;

            var data = BrainEditorWindow.Instance.State.CurrentNetworkAsset;
            var customRect = new Rect(args.position, args.size);
            EditorGUI.DrawRect(customRect, new Color(0.2196079f, 0.42f, 0.2196079f, 1f));

            GUILayout.BeginArea(customRect, new GUIStyle { contentOffset = new Vector2(15, 15) });
            
            GUILayout.BeginVertical();

            for (var i = 0; i < data.inputNeurons.Count; i++)
            {
                GUILayout.BeginHorizontal();
                data.inputNeurons[i].data = EditorGUILayout.DoubleField(data.inputNeurons[i].data);
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Feed Forward"))
            {
                var inputs = data.inputNeurons.Select(s => s.data);
                var output = BrainEditorWindow.Instance.State.brainController.FeedForward(inputs.ToArray());
                for (var i = 0; i < output.Length; i++) Debug.Log($"output[{i}] = {output[i]}");
            }
            
            GUILayout.EndVertical();

            GUILayout.EndArea();
        }
    }
}