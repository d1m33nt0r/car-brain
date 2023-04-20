using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class GraphDrawer : StylizedDrawer<GraphModel>, IRectArea
    {
        public Rect Rect => rect;
        
        private Rect rect;
        private GridDrawer gridDrawer;
        
        protected override void ApplyStyles()
        {
            rect = new Rect(new Vector2(240, 0), Vector2.one * 10000);
            gridDrawer = new GridDrawer(Rect);
        }

        public void OnDragCallback(Vector2 delta)
        {
            gridDrawer.OnDrag(delta);
        }

        public override void Draw(GraphModel args)
        {
            EditorZoomArea.Begin(args.zoom, rect);
            //GUILayout.BeginArea(rect);
            
            gridDrawer.Draw();
            
            if (args.NodesDrawers == null) return;
            
            for (var i = 0; i < args.NodesDrawers.Count; i++)
            {
                args.NodesDrawers[i].Draw(args.NodesModels[i]);
            }

            if (args.ConnectionDrawers != null)
            {
                for (var i = 0; i < args.ConnectionDrawers.Count; i++)
                {
                    args.ConnectionDrawers[i].Draw(args.ConnectionModels[i]);
                }
            }
            //GUILayout.EndArea();
            EditorZoomArea.End();
        }
    }
}