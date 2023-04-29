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
                var path = EditorUtility.OpenFilePanel("Choose neural network asset", "Assets", "");
                var s = JsonUtility.FromJson<NeuralNetworkData>(File.ReadAllText(path));
                BrainEditorWindow.Instance.State.CurrentNetworkAsset = s;
                OnChangedCurrentNetworkAsset?.Invoke();
            }
            
            if (GUILayout.Button("Save As"))
            {
                var s = EditorUtility.SaveFilePanel("Save network asset", "Assets", "BrainAsset", "json");
                if (s.Length > 0) File.WriteAllText(s, JsonUtility.ToJson(BrainEditorWindow.Instance.State.CurrentNetworkAsset));
            }

            GUILayout.EndHorizontal();
        }
    }
}