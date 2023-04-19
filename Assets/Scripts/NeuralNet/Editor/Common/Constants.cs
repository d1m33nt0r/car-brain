namespace NeuralNet.Editor
{
    public static class Constants
    {
        public class General
        {
            public const string WINDOW_NAME = "Neural Editor";
            public const string MENU_PATH = "Brain/Neural Editor";
            public const string DEFAULT_NODE_TITLE = "Node";
            public const string NODE_SUFFIX = "_";
            public static readonly (int width, int height) DEFAULT_NODE_SIZE = (200, 50);
            public static readonly (int width, int height) DEFAULT_CONNECTION_POINT_SIZE = (10, 20);
        }
    }
}