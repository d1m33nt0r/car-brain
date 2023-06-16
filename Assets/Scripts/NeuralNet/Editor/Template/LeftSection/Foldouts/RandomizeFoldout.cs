using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class RandomizeFoldout : StylizedDrawer<EmptyArgs, EmptyArgs>
    {
        private float min;
        private float max;

        private int chanceRandomization;
        private bool isChancedRandomization;
        
        protected override void ApplyStyles(EmptyArgs args)
        {
            
        }

        public override void Draw(EmptyArgs args)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Min range");
            min = EditorGUILayout.FloatField(min);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Max range");
            max = EditorGUILayout.FloatField(max);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Probabilistic randomization");
            GUILayout.Space(19);
            isChancedRandomization = EditorGUILayout.Toggle(isChancedRandomization);
            GUILayout.EndHorizontal();

            if (isChancedRandomization)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Chance");
                chanceRandomization = EditorGUILayout.IntSlider(chanceRandomization, 0, 100);
                GUILayout.Label("%");
                GUILayout.EndHorizontal();
            }
            
            if (GUILayout.Button("Randomize"))
            {
                
            }
        }

        public RandomizeFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
        }
    }
}