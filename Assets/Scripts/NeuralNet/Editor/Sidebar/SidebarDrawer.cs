using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.Sidebar.MainSection;
using UnityEngine;

namespace NeuralNet.Editor.Sidebar
{
    public class SidebarDrawer : StylizedDrawer<SidebarModel>
    {
        private Rect rect;
        private MainSectionDrawer mainSectionDrawer;
        private MainSectionModel mainSectionModel;
        
        public SidebarDrawer(MainSectionModel mainSectionModel, MainSectionDrawer mainSectionDrawer)
        {
            this.mainSectionDrawer = mainSectionDrawer;
            this.mainSectionModel = mainSectionModel;
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