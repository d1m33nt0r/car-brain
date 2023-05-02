using System;
using System.IO;
using NeuralNet.Core;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template
{
    public class TopMenu : StylizedDrawer<EmptyDrawerArgs>
    {
        public event Action OnChangedCurrentNetworkAsset; 
        protected override void ApplyStyles()
        {
            rect = new Rect(new Vector2(0, 0), new Vector2(BrainEditorWindow.Instance.position.width, 23));
        }

        public override void Draw(EmptyDrawerArgs args)
        {
            EditorGUI.DrawRect(rect, new Color(0.2196079f, 0.2196079f, 0.2196079f, 1f));
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Open"))
            {
                var path = EditorUtility.OpenFilePanel("Choose neural network asset", "Assets", "json");
                var s = Serializer.ReadFromJson(path);
                BrainEditorWindow.Instance.State.CurrentNetworkAsset = s;
                OnChangedCurrentNetworkAsset?.Invoke();
            }
            
            if (GUILayout.Button("Save As"))
            {
                var s = EditorUtility.SaveFilePanel("Save network asset", "Assets", "BrainAsset", "json");
                if (s.Length > 0) Serializer.WriteToJson(s, BrainEditorWindow.Instance.State.CurrentNetworkAsset, true);
            }
            
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

            GUILayout.EndHorizontal();
        }
    }
}