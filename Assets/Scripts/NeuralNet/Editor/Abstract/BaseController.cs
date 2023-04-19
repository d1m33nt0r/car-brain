namespace NeuralNet.Editor.Abstract
{
    public abstract class BaseController<TView, TModel> where TView : IDrawer<TModel> where TModel : BaseModel
    {
        protected TModel model;
        protected TView view;

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