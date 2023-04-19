using NeuralNet.Editor.NeuralNetwork;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor
{
    public class NeuralEditorWindow : EditorWindowSingleton<NeuralEditorWindow>
    {
        protected override string windowTitle { get; } = Constants.General.WINDOW_NAME;

        private GridDrawer gridDrawer;
        private NeuralNetworkDrawer neuralNetworkDrawer;
        private NeuralNetworkModel neuralNetworkModel;
        private NeuralNetworkController neuralNetworkController;
        
        [MenuItem(Constants.General.MENU_PATH)]
        private static void ShowWindow()
        {
            var window = Instance;
        }

        private void OnEnable()
        {
            gridDrawer = new GridDrawer(this);
            neuralNetworkDrawer = new NeuralNetworkDrawer();
            neuralNetworkModel = new NeuralNetworkModel();
            neuralNetworkController = new NeuralNetworkController();
            neuralNetworkController.AttachModel(neuralNetworkModel);
            neuralNetworkController.AttachDrawer(neuralNetworkDrawer);
        }

        private void OnGUI()
        {
            neuralNetworkController.ProcessEvents(Event.current);
            neuralNetworkController.ProcessNodesEvents(Event.current);
            gridDrawer.Draw();
            neuralNetworkDrawer.Draw(neuralNetworkModel);
        }
    }
}