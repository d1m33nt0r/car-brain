namespace NeuralNet.Core.Abstract
{
    public interface IFactory<out TController, TModel, in TSelfArgs> where TController : BaseController<TModel> where TModel : BaseModel where TSelfArgs : BaseArgs
    {
        TController Create(TSelfArgs args = null);
    }
}