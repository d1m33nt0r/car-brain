using NeuralNet.Core.Activations;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common.Interfaces;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class LayerFoldout : BaseFoldout<EmptyArgs, EmptyArgs>, IInitializable<LayerFoldoutDrawArg>
    {
        private int index;
        private NewAssetData data;
        
        public LayerFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
        }

        protected override void ApplyStyles(EmptyArgs args)
        {
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.23f, 0.23f, 0.23f, 1f));
            canvasBackground.Apply();
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(2, 2, 2, 2);
            foldoutStyle.margin = new RectOffset(20, 0, 4, 4);
            foldoutStyle.normal.background = canvasBackground;
        }

        protected override void DrawInternal(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Neurons count");
            data.hiddenLayers[index] = EditorGUILayout.IntField(data.hiddenLayers[index]);
            GUILayout.EndHorizontal();
                
            GUILayout.BeginHorizontal();
            GUILayout.Label("Activations");
            data.hiddenActivationTypes[index] = (ActivationType) EditorGUILayout.EnumPopup(data.hiddenActivationTypes[index]);
            GUILayout.EndHorizontal();
        }

    

        public void Initialize(LayerFoldoutDrawArg args)
        {
            data = args.data;
            index = args.index;
        }
    }
}