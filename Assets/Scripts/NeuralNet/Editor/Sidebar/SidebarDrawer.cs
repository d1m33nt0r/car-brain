using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Services;
using NeuralNet.Editor.Sidebar.MainSection;
using UnityEngine;

namespace NeuralNet.Editor.Sidebar
{
    public class SidebarDrawer : StylizedDrawer<SidebarModel>
    {
        private Rect rect;
        private MainSectionDrawer mainSectionDrawer;
        private MainSectionModel mainSectionModel;
        
        public SidebarDrawer()
        {
            mainSectionDrawer = ServiceLocator.Instance.GetService<MainSectionDrawer>();
            mainSectionModel = ServiceLocator.Instance.GetService<MainSectionModel>();
        }
        
        protected override void ApplyStyles()
        {
            var width = NeuralEditorWindow.Instance.position.width - (NeuralEditorWindow.Instance.position.width - 240);
            rect = new Rect(Vector2.zero, new Vector2(width, NeuralEditorWindow.Instance.position.height));
        }

        public override void Draw(SidebarModel args)
        {
            GUILayout.BeginArea(rect);
            
            mainSectionDrawer.Draw(mainSectionModel);
            
            GUILayout.EndArea();
        }
    }
}