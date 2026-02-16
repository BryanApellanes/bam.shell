namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for the result of executing an input command.
    /// </summary>
    public interface IInputCommandResult
    {
        /// <summary>
        /// Gets or sets the name of the input command that was executed.
        /// </summary>
        string InputName { get; set; }

        /// <summary>
        /// Gets the result object returned by the command invocation.
        /// </summary>
        object? InvocationResult { get; }

        /// <summary>
        /// Gets a value indicating whether the command executed successfully.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the message describing the result or error.
        /// </summary>
        string? Message { get; }

        /// <summary>
        /// Gets or sets the exception that occurred during execution, if any.
        /// </summary>
        Exception Exception { get; set; }
    }
}
