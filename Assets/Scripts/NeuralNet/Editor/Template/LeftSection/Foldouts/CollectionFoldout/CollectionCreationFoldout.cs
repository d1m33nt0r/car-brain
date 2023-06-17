using NeuralNet.Editor.Args;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Template.LeftSection.Foldouts.CollectionFoldout
{
    public class CollectionCreationFoldout : BaseFoldout<EmptyArgs, EmptyArgs>
    {
        private string collectionName;
        private Texture2D applyIcon;
        
        public CollectionCreationFoldout(EmptyArgs styleArgs) : base(styleArgs)
        {
        }

        protected override void ApplyStyles(EmptyArgs args)
        {
            var path = "Assets/Scripts/NeuralNet/Editor/Resources/apply_icon.png";
            applyIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            var canvasBackground = new Texture2D(1, 1);
            canvasBackground.SetPixel(1, 1, new Color(0.25f, 0.25f, 0.25f, 1f));
            canvasBackground.Apply();
            var borderSize = 5;
            foldoutStyle = new GUIStyle();
            foldoutStyle.padding = new RectOffset(borderSize, borderSize, borderSize, borderSize);
            foldoutStyle.normal.background = canvasBackground;
        }

        protected override void Draw()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name", GUILayout.Height(25), GUILayout.Width(36));
            collectionName = GUILayout.TextField(collectionName, GUILayout.Height(25));
            if (GUILayout.Button(applyIcon, GUILayout.Width(24), GUILayout.Height(25)))
            {
                var editorData = BrainEditorWindow.Instance.State.EditorData;
                if (!editorData.collections.Exists(i => i.name == collectionName))
                {
                    AssetDatabase.CreateFolder("Assets/Plugins/EasyBrain/Editor/Data/Collections", collectionName);
                    var collectionInstance = ScriptableObject.CreateInstance<BrainCollection>();
                    collectionInstance.name = collectionName;
                    AssetDatabase.CreateAsset(collectionInstance, "Assets/Plugins/EasyBrain/Editor/Data/Collections" + "/" + collectionName + "/" + collectionName + ".asset");
                    AssetDatabase.SaveAssets();
                    editorData.collections.Add(collectionInstance);
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}