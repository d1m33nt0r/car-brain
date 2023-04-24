using System.Collections.Generic;
using System.IO;
using NeuralNet.Core;
using NeuralNet.Core.Layers;
using NeuralNet.Core.Neurons.Output;
using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.NeuralNetwork;
using NeuralNet.Editor.Services;
using NeuralNet.Editor.Workspace;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace NeuralNet.Editor.Sidebar.MainSection
{
    public class MainSectionDrawer : StylizedDrawer<MainSectionModel>
    {
        private Rect rect;
        private WindowState state;
        private WorkspaceModel workspaceModel;
        public MainSectionDrawer()
        {
            state = ServiceLocator.Instance.GetService<WindowState>();
            workspaceModel = ServiceLocator.Instance.GetService<WorkspaceModel>();
        }
        
        protected override void ApplyStyles()
        {
            rect = new Rect(new Vector2(10, 20), new Vector2(220, 200));
        }

        public override void Draw(MainSectionModel args)
        {
            GUILayout.BeginArea(rect);
            
            if (GUILayout.Button("Import network"))
            {
                var path = EditorUtility.OpenFilePanel("Choose neural network asset", "Assets", "json");
                state.SelectedNetworkModel = JsonConvert.DeserializeObject<NeuralNetworkModel>(path);
                workspaceModel.GraphModel = new GraphModel(state.SelectedNetworkModel);
            }
            
            if (GUILayout.Button("Export network"))
            {
                var s = EditorUtility.SaveFilePanel("Save network asset", "Assets", "brain", "json");
                var inputLayer = new LayerModel<InputNeuronModel>(new List<InputNeuronModel>(), 0);
                var weightedLayers = new List<LayerModel<WeightedNeuronModel>>();
                var outputLayer = new LayerModel<WeightedNeuronModel>(new List<WeightedNeuronModel>(), 0);
                var neuralNetworkModel = new NeuralNetworkModel(inputLayer, weightedLayers, outputLayer);
                if (s.Length > 0) File.WriteAllText(s, JsonConvert.SerializeObject(neuralNetworkModel));
            }
            
            GUILayout.EndArea();
        }
    }
}