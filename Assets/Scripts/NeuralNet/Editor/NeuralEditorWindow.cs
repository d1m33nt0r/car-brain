using NeuralNet.Editor.Sidebar;
using NeuralNet.Editor.Sidebar.MainSection;
using NeuralNet.Editor.Workspace;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class NeuralEditorWindow : EditorWindowSingleton<NeuralEditorWindow>
    {
        public WindowState state { get; private set; }
        protected override string windowTitle { get; } = Constants.General.WINDOW_NAME;
        
        private WorkspaceDrawer workspaceDrawer;
        private WorkspaceModel workspaceModel;
        private WorkspaceController workspaceController;

        private SidebarController sidebarController;
        private SidebarDrawer sidebarDrawer;
        private SidebarModel sidebarModel;

        private MainSectionController mainSectionController;
        private MainSectionDrawer mainSectionDrawer;
        private MainSectionModel mainSectionModel;

        [MenuItem(Constants.General.MENU_PATH)]
        private static void ShowWindow()
        {
            var window = Instance;
        }

        private void OnEnable()
        {
            state = new WindowState();
            
            mainSectionModel = new MainSectionModel();
            mainSectionDrawer = new MainSectionDrawer();
            mainSectionController = new MainSectionController();
            mainSectionController.AttachDrawer(mainSectionDrawer);
            mainSectionController.AttachModel(mainSectionModel);
            
            workspaceModel = new WorkspaceModel();
            workspaceDrawer = new WorkspaceDrawer();
            workspaceController = new WorkspaceController();
            workspaceController.AttachDrawer(workspaceDrawer);
            workspaceController.AttachModel(workspaceModel);
            
            sidebarController = new SidebarController();
            sidebarDrawer = new SidebarDrawer(mainSectionModel, mainSectionDrawer);
            sidebarModel = new SidebarModel();
            sidebarController.AttachDrawer(sidebarDrawer);
            sidebarController.AttachModel(sidebarModel);
        }
        
        private void OnGUI()
        {
            workspaceController.ProcessEvents(Event.current);
            workspaceDrawer.Draw(workspaceModel);
            sidebarDrawer.Draw(sidebarModel);
        }
    }
}