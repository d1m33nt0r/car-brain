namespace NeuralNet.Editor.Abstract
{
    public abstract class BaseController<TView, TModel> where TView : IDrawer<TModel> where TModel : BaseModel
    {
        public TModel model;
        public TView view;

        public virtual void AttachDrawer(TView view)
        {
            this.view = view;
        }

        public virtual void AttachModel(TModel model)
        {
            this.model = model;
        }
    }
}