namespace Bam.Shell
{
    public interface IInputCommandResult
    {       
        string InputName { get; set; }
        object? InvocationResult { get; }
        bool Success { get; }
        string? Message { get; }
        Exception Exception { get; set; }
    }
}
