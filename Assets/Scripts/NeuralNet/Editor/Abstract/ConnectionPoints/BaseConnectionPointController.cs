using System;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public class BaseConnectionPointController : BaseController<BaseConnectionPointDrawer, BaseConnectionPointModel>
    {
        public event Action<BaseConnectionPointController> OnClickConnectionPoint;

        public override void AttachDrawer(BaseConnectionPointDrawer view)
        {
            base.AttachDrawer(view);
            view.OnClickConnectionPoint += OnClick;
        }

        private void OnClick(ConnectionPointType connectionPointType)
        {
            OnClickConnectionPoint?.Invoke(this);
        }
    }
}