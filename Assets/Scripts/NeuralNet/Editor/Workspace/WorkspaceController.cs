using NeuralNet.Editor.Abstract;
using UnityEngine;

namespace NeuralNet.Editor.Workspace
{
    public class WorkspaceController : BaseController<WorkspaceDrawer, WorkspaceModel>
    {
        public void ProcessEvents(Event e)
        {
            model.GraphController.ProcessEvents(Event.current);
            model.GraphController.ProcessNodesEvents(Event.current);
        }
    }
}