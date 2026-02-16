namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for the result of running a menu item.
    /// </summary>
    public interface IMenuItemRunResult
    {
        /// <summary>
        /// Gets the result object returned by the menu item's method.
        /// </summary>
        object? Result { get; }

        /// <summary>
        /// Gets the menu input that was used when running the item.
        /// </summary>
        IMenuInput? MenuInput { get; }

        /// <summary>
        /// Gets the menu item that was run.
        /// </summary>
        IMenuItem? MenuItem { get; }

        /// <summary>
        /// Gets a value indicating whether the menu item executed successfully.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the message describing the result or error.
        /// </summary>
        string? Message { get; }

        /// <summary>
        /// Gets the exception that occurred during execution, if any.
        /// </summary>
        Exception? Exception { get; }
    }
}
