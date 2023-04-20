using System;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public class BaseConnectionController : BaseController<BaseConnectionDrawer, BaseConnectionModel>
    {
        public event Action<BaseConnectionController, BaseConnectionDrawer, BaseConnectionModel> OnRemoveConnectionClicked;
        
        public override void AttachDrawer(BaseConnectionDrawer view)
        {
            base.AttachDrawer(view);
            view.OnClickRemoveConnection += OnClickRemoveConnection;
        }

        private void OnClickRemoveConnection()
        {
            OnRemoveConnectionClicked?.Invoke(this, view, model);
        }
    }
}