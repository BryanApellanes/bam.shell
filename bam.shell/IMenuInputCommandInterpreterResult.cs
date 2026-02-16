namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for the result of interpreting menu input commands.
    /// </summary>
    public interface IMenuInputCommandInterpreterResult
    {
        /// <summary>
        /// Gets the collection of command results from interpretation.
        /// </summary>
        IEnumerable<IInputCommandResult?> MenuItemRunResults { get; }

        /// <summary>
        /// Adds an object result, wrapping it in a successful <see cref="MenuItemRunResult"/>.
        /// </summary>
        /// <param name="result">The result object to add.</param>
        /// <returns>This instance for fluent chaining.</returns>
        MenuInputCommandInterpreterResult AddResult(object? result);

        /// <summary>
        /// Adds an input command result to the collection.
        /// </summary>
        /// <param name="result">The input command result to add.</param>
        /// <returns>This instance for fluent chaining.</returns>
        MenuInputCommandInterpreterResult AddResult(IInputCommandResult? result);
    }
}
