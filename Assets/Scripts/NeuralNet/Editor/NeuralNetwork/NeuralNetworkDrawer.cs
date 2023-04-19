using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.NeuralNetwork
{
    public class NeuralNetworkDrawer : IDrawer<NeuralNetworkModel>
    {
        public void Draw(NeuralNetworkModel args)
        {
            if (args.NodesDrawers == null) return;
            
            for (var i = 0; i < args.NodesDrawers.Count; i++)
            {
                args.NodesDrawers[i].Draw(args.NodesModels[i]);
            }
        }
    }
}