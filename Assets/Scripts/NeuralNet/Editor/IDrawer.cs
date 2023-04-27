namespace NeuralNet.Editor
{
    public interface IDrawer<TArgs> where TArgs : BaseDrawerArgs
    {
        void Draw(TArgs args);
    }
}