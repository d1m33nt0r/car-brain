using System;
using NeuralNet.Editor.Abstract;

namespace NeuralNet.Editor.Connections
{
    public abstract class BaseConnectionPointController : BaseController<BaseConnectionPointDrawer, BaseConnectionPointModel>
    {
        public event Action<BaseConnectionPointModel> OnClickConnectionPoint;

        public override void AttachDrawer(BaseConnectionPointDrawer view)
        {
            base.AttachDrawer(view);
            view.OnClickConnectionPoint += OnClick;
        }

        private void OnClick()
        {
            OnClickConnectionPoint?.Invoke(model);
        }
    }
}