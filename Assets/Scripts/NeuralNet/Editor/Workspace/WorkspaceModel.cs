using NeuralNet.Editor.Abstract;
using NeuralNet.Editor.NeuralNetwork;

namespace NeuralNet.Editor.Workspace
{
    public class WorkspaceModel : BaseModel
    {
        public GraphController GraphController;
        public GraphDrawer GraphDrawer;
        public GraphModel GraphModel;

        public WorkspaceModel()
        {
            GraphController = new GraphController();
            GraphDrawer = new GraphDrawer();
            GraphModel = new GraphModel();
            GraphController.AttachDrawer(GraphDrawer);
            GraphController.AttachModel(GraphModel);
        }

        public WorkspaceModel(GraphController graphController, GraphDrawer graphDrawer, GraphModel graphModel)
        {
            GraphController = graphController;
            GraphDrawer = graphDrawer;
            GraphModel = graphModel;
            graphController.AttachDrawer(graphDrawer);
            graphController.AttachModel(graphModel);
        }
    }
}