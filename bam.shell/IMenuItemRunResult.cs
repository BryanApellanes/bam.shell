namespace Bam.Shell
{
    public interface IMenuItemRunResult
    {
        object? Result { get; }
        IMenuInput? MenuInput { get; }
        IMenuItem? MenuItem { get; }
        bool Success { get; }
        string? Message { get; }
        Exception? Exception { get; }
    }
}
