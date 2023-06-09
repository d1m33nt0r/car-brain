using NeuralNet.Editor.Args;
using Unity.Mathematics;
using UnityEngine;

namespace NeuralNet.Editor.Common
{
    public static class Constants
    {
        public class Window
        {
            public static readonly Vector2 minSize = new Vector2(200, 100);
            public static readonly string editorDataFolderPath = "Assets/Plugins/EasyBrain/Editor";
            public static readonly string editorDataFolderName = "Data";
        }
        
        public class LeftSection
        {
            public static readonly float MIN_WIDTH = 200;
        }

        public class BottomSection
        {
            public static readonly float MIN_HEIGHT = 200;
        }

        public class Args
        {
            public static readonly EmptyArgs EmptyArgs = new ();
        }
    }
}