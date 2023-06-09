using System.Collections.Generic;
using NeuralNet.Core;
using NeuralNet.Core.Activations;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template
{
    public class NewAssetPopup : StylizedDrawer<NewAssetPopupDrawArgs>
    {
        private float height;
        
        protected override void ApplyStyles()
        {
            
        }

        public override void Draw(NewAssetPopupDrawArgs drawArgs)
        {
            if (!BrainEditorWindow.Instance.State.showAssetPopup) return;
            
            var data = BrainEditorWindow.Instance.State.NewAssetData;
            var customRect = new Rect(new Vector2(0, 23), new Vector2(drawArgs.width, 145+ 40 * data.hiddens));
            EditorGUI.DrawRect(customRect, new Color(0.2196079f, 0.42f, 0.2196079f, 1f));
            
            GUILayout.BeginArea(customRect, new GUIStyle{ contentOffset = new Vector2(15, 15)});

            GUILayout.BeginHorizontal();
            GUILayout.Label("Input neurons");
            data.inputs = EditorGUILayout.IntField(data.inputs);
            GUILayout.EndHorizontal();
            
            var prevHiddens = data.hiddens;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Hidden layers");
            data.hiddens = EditorGUILayout.IntField(data.hiddens);
            if (prevHiddens != data.hiddens)
            {
                data.hiddenLayers = new List<int>();
                data.hiddenActivationTypes = new List<ActivationType>();
                for (var i = 0; i < data.hiddens; i++)
                {
                    data.hiddenLayers.Add(0);
                    data.hiddenActivationTypes.Add(default);
                }
            }
            GUILayout.EndHorizontal();
            
            for (var i = 0; i < data.hiddens; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Hidden layer {i}");
                data.hiddenLayers[i] = EditorGUILayout.IntField(data.hiddenLayers[i]);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label($"Activation hidden {i}");
                data.hiddenActivationTypes[i] = (ActivationType) EditorGUILayout.EnumPopup(data.hiddenActivationTypes[i]);
                GUILayout.EndHorizontal();
            }
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Output neurons");
            data.outputs = EditorGUILayout.IntField(data.outputs);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Output activation");
            data.outputActivationType = (ActivationType) EditorGUILayout.EnumPopup(data.outputActivationType);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Output activation");
            data.neuronRandomRange = EditorGUILayout.FloatField(data.neuronRandomRange);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Weight value range");
            data.weightRandomRange = EditorGUILayout.FloatField(data.weightRandomRange);
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Generate"))
            {
                var asset = ScriptableObject.CreateInstance<NeuralNetworkData>();
                asset.Init(data.inputs, data.outputs,
                    data.hiddenLayers.ToArray(), data.outputActivationType,
                    data.hiddenActivationTypes.ToArray(), data.neuronRandomRange, data.weightRandomRange);
                BrainEditorWindow.Instance.State.SetCurrentNetworkAsset(asset);
            }
            
            GUILayout.EndArea();
        }
    }
}