namespace Bam.Shell
{
    public interface IMenuInputReader
    {
        event EventHandler<MenuInputOutputLoopEventArgs> ReadingInput;
        IMenuInput ReadMenuInput();
    }
}
