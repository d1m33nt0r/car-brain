using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Workspace
{
    public class WorkspaceDrawer : StylizedDrawer<WorkspaceModel>, IRectArea
    {
        public Rect Rect { get => rect; }

        private Rect rect;
        private GridDrawer gridDrawer;
        
        protected override void ApplyStyles()
        {
            rect = new Rect(Vector2.zero, Vector2.one * 540);
            gridDrawer = new GridDrawer(Rect);
        }

        public override void Draw(WorkspaceModel args)
        {
            gridDrawer.Draw();
            args.GraphDrawer.Draw(args.GraphModel);
        }
    }
}