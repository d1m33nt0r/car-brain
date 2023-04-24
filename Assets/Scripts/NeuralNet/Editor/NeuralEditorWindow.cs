using NeuralNet.Editor.Services;
using NeuralNet.Editor.Sidebar;
using NeuralNet.Editor.Sidebar.MainSection;
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

        private SidebarController sidebarController;
        private SidebarDrawer sidebarDrawer;
        private SidebarModel sidebarModel;

        private MainSectionController mainSectionController;
        private MainSectionDrawer mainSectionDrawer;
        private MainSectionModel mainSectionModel;

        private WindowState state;
        
        [MenuItem(Constants.General.MENU_PATH)]
        private static void ShowWindow()
        {
            var window = Instance;
        }

        private void OnEnable()
        {
            state = new WindowState();
            ServiceLocator.Instance.Register(state);
            workspaceModel = new WorkspaceModel();
            ServiceLocator.Instance.Register(workspaceModel);
            mainSectionDrawer = new MainSectionDrawer();
            ServiceLocator.Instance.Register(mainSectionDrawer);
            
            mainSectionController = new MainSectionController();
            mainSectionModel = new MainSectionModel();
            ServiceLocator.Instance.Register(mainSectionModel);
            mainSectionController.AttachDrawer(mainSectionDrawer);
            mainSectionController.AttachModel(mainSectionModel);
            
            workspaceDrawer = new WorkspaceDrawer();
            workspaceController = new WorkspaceController();
            
            sidebarController = new SidebarController();
            sidebarDrawer = new SidebarDrawer();
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