using NeuralNet.Editor.Workspace;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class NeuralEditorWindow : EditorWindowSingleton<NeuralEditorWindow>
    {
        protected override string windowTitle { get; } = Constants.General.WINDOW_NAME;
        
        private WorkspaceDrawer workspaceDrawer;
        private WorkspaceModel workspaceModel;
        private WorkspaceController workspaceController;
        
        [MenuItem(Constants.General.MENU_PATH)]
        private static void ShowWindow()
        {
            var window = Instance;
        }

        private void OnEnable()
        {
            workspaceModel = new WorkspaceModel();
            workspaceDrawer = new WorkspaceDrawer();
            workspaceController = new WorkspaceController();
            workspaceController.AttachDrawer(workspaceDrawer);
            workspaceController.AttachModel(workspaceModel);
        }

        private void OnGUI()
        {
            workspaceController.ProcessEvents(Event.current);
            workspaceDrawer.Draw(workspaceModel);
        }
    }
}