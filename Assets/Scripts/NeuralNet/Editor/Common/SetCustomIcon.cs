using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class SetCustomIcon : AssetModificationProcessor
    {
        /*public static void OnWillCreateAsset(string path)
        {
            // Перевірка розширення файлу
            if (!path.EndsWith(".brain")) 
                return;
        
            // Створення іконки
            Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Scripts/NeuralNet/Editor/Resources/brain.png");

            // Перевірка, чи було завантажене зображення
            if (icon == null)
            {
                Debug.LogWarning("myIcon.png не знайдено в Assets/Scripts/NeuralNet/Editor/Resources/brain.png");
                return;
            }
        
            // Призначення іконки
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.spritePixelsPerUnit = 100;
            textureImporter.spritePivot = new Vector2(0.5f, 0.5f);
            textureImporter.spriteBorder = new Vector4(0, 0, 0, 0);
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.SaveAndReimport();
        
            // Оновлення елементів редактора Unity
            AssetDatabase.Refresh();
        }*/
    }
}