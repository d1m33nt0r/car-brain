using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public abstract class EditorWindowSingleton<TSelfType> : EditorWindow where TSelfType : EditorWindowSingleton<TSelfType>
    {
        protected virtual string windowTitle => "window title";
        private static TSelfType instance;

        public static TSelfType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GetWindow<TSelfType>();
                    instance.titleContent = new GUIContent(instance.windowTitle);
                }

                return instance;
            }
        }
    }
}