namespace NeuralNet.Editor.Args.Draw
{
    public class TestViewDrawArgs : BaseArgs
    {
        public bool testModeIsActive;

        public TestViewDrawArgs(bool testModeIsActive)
        {
            this.testModeIsActive = testModeIsActive;
        }
    }
}