namespace NeuralNet.Editor.Abstract
{
    public interface IDrawer<TSelfArgs> where TSelfArgs : BaseModel
    {
        void Draw(TSelfArgs args);
    }
}