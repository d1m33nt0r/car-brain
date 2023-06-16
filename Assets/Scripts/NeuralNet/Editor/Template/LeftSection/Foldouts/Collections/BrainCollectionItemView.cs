using NeuralNet.Core;
using NeuralNet.Editor.Args;
using NeuralNet.Editor.Args.Draw;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts.Collections
{
    public class BrainCollectionItemView : StylizedDrawer<EmptyArgs, BrainCollectionItemDrawArgs>
    {
        private BrainData selectedObject;
        
        protected override void ApplyStyles(EmptyArgs args)
        {
            
        }

        public override void Draw(BrainCollectionItemDrawArgs args)
        {
            GUILayout.BeginHorizontal();
            
            selectedObject = (BrainData) EditorGUILayout.ObjectField(selectedObject, typeof(BrainData));
            GUI.Button(new Rect(new Vector2(0, 0), new Vector2(28, 20)), "s");

            
            GUILayout.EndHorizontal();
        }

        public BrainCollectionItemView(EmptyArgs styleArgs) : base(styleArgs)
        {
        }
    }
}