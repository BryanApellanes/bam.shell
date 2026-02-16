namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for a collection of input command results.
    /// </summary>
    public interface IInputCommandResults
    {
        /// <summary>
        /// Gets or sets the aggregate exception from all results, if any.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the overall message for the results.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets the collection of individual input command results.
        /// </summary>
        IEnumerable<IInputCommandResult> Results { get; }

        /// <summary>
        /// Gets a value indicating whether all commands executed successfully.
        /// </summary>
        bool Sucess { get; }

        /// <summary>
        /// Adds an input command result to the collection.
        /// </summary>
        /// <param name="result">The result to add.</param>
        void AddResult(IInputCommandResult result);
    }
}