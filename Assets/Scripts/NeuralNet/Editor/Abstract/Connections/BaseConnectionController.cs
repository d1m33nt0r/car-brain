using System;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public class BaseConnectionController : BaseController<BaseConnectionDrawer, BaseConnectionModel>
    {
        public event Action OnConnectionClicked;
        
        public override void AttachDrawer(BaseConnectionDrawer view)
        {
            base.AttachDrawer(view);
            view.OnClickConnection += OnClickConnection;
        }

        private void OnClickConnection()
        {
            OnConnectionClicked?.Invoke();
        }
    }
}