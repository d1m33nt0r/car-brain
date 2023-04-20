using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class GraphDrawer : IDrawer<GraphModel>
    {
        public void Draw(GraphModel args)
        {
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
        }
    }
}