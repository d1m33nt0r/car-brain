using UnityEditor;

namespace NN.Editor
{
    public class NeuralEditorWindow : EditorWindowSingleton<NeuralEditorWindow>
    {
        protected override string windowTitle { get; } = Constants.General.WINDOW_NAME;

        private GridDrawer gridDrawer;
        
        [MenuItem(Constants.General.MENU_PATH)]
        private static void ShowWindow()
        {
            var window = Instance;
        }

        private void OnEnable()
        {
            gridDrawer = new GridDrawer(this);
        }

        private void OnGUI()
        {
            gridDrawer.Draw();
        }
    }
}