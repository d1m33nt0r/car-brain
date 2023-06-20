using System.Collections.Generic;
using NeuralNet.Core;
using NeuralNet.Core.Activations;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class FullPerceptronTemplate : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<EmptyArgs>
    {
        private NewAssetData data;
        private List<LayerFoldout> layers = new();

        protected override void ApplyStyles(EmptyArgs styleArgs)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.21f, 0.21f, 0.21f, 1f));
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.normal.background = canvasBackground;
        }
        
        protected override void DrawInternal(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Count Input Neurons");
            data.inputs = EditorGUILayout.IntField(data.inputs);
            GUILayout.EndHorizontal();
            
            var prevHiddens = data.hiddens;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Count Hidden Layers");
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
                
                OnChangedHiddenLayersCount();
            }
            GUILayout.EndHorizontal();
            
            for (var i = 0; i < data.hiddens; i++)
            {
                layers[i].Draw(Constants.Args.EmptyArgs);
            }
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Count Output Neurons");
            data.outputs = EditorGUILayout.IntField(data.outputs);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Output Neurons Activations");
            data.outputActivationType = (ActivationType) EditorGUILayout.EnumPopup(data.outputActivationType);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Weight Range");
            data.weightRandomRange = EditorGUILayout.FloatField(data.weightRandomRange);
            GUILayout.EndHorizontal();
            
            /*if (GUILayout.Button("Generate"))
            {
                var asset = ScriptableObject.CreateInstance<BrainData>();
                asset.Init(data.inputs, data.outputs,
                    data.hiddenLayers.ToArray(), data.outputActivationType,
                    data.hiddenActivationTypes.ToArray(), data.neuronRandomRange, data.weightRandomRange);
                BrainEditorWindow.Instance.State.SetCurrentNetworkAsset(asset);
            }*/
        }

        private void OnChangedHiddenLayersCount()
        {
            layers = new List<LayerFoldout>();
            for (var i = 0; i < data.hiddens; i++)
            {
                layers.Add(new LayerFoldout(Constants.Args.EmptyArgs));
                layers[i].Initialize(new LayerFoldoutDrawArg(data, i));
            }
        }
        
        public void Initialize(EmptyArgs args)
        {
            data = BrainEditorWindow.Instance.State.NewAssetData;
        }

        public FullPerceptronTemplate(EmptyArgs styleArgs) : base(styleArgs)
        {
        }
    }
}