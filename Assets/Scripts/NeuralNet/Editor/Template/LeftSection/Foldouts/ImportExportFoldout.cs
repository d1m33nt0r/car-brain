using NeuralNet.Editor.Args;
using NeuralNet.Editor.Common.Abstract;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts
{
    public class ImportExportFoldout : StylizedDrawer<EmptyArgs, EmptyArgs>
    {
        protected override void ApplyStyles(EmptyArgs args)
        {
            
        }

        public override void Draw(EmptyArgs args)
        {
            if (GUILayout.Button("Import from JSON"))
            {
                var path = EditorUtility.OpenFilePanel("Choose neural network asset", "Assets", "json");
                if (path.Length > 0)
                {
                    var s = EditorSerializer.ReadFromJson(path);
                    BrainEditorWindow.Instance.State.SetCurrentNetworkAsset(s);
                }
            }

            if (GUILayout.Button("Export to JSON"))
            {
                
            }
            
            if (GUILayout.Button("Import from binary"))
            {
                
            }
            
            if (GUILayout.Button("Export to binary"))
            {
                
            }
        }

        public ImportExportFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
        }
    }
}