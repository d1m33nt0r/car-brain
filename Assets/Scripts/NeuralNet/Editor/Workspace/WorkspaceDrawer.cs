using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Workspace
{
    public class WorkspaceDrawer : StylizedDrawer<WorkspaceModel>, IRectArea
    {
        public Rect Rect { get => rect; }
        private Rect rect;
        
        protected override void ApplyStyles()
        {
            
        }
        
        public override void Draw(WorkspaceModel args)
        {
            args.GraphDrawer.Draw(args.GraphModel);
        }
    }
}