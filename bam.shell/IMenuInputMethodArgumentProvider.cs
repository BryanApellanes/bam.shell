namespace Bam.Shell
{
    public interface IMenuInputMethodArgumentProvider
    {
        object?[] GetMethodArguments(IMenuItem menuItem, IMenuInput menuInput);
    }
}
