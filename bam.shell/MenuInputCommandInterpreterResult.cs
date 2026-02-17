namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuInputCommandInterpreterResult"/> that collects command results.
    /// </summary>
    public class MenuInputCommandInterpreterResult : IMenuInputCommandInterpreterResult
    {
        List<IInputCommandResult> menuItemRunResults = new List<IInputCommandResult>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuInputCommandInterpreterResult"/> class.
        /// </summary>
        public MenuInputCommandInterpreterResult()
        {
        }

        /// <summary>
        /// Gets the collection of command results.
        /// </summary>
        public IEnumerable<IInputCommandResult?> MenuItemRunResults
        {
            get
            {
                return menuItemRunResults;
            }
        }

        /// <summary>
        /// Adds an object result, wrapping it in a successful <see cref="MenuItemRunResult"/>.
        /// </summary>
        /// <param name="result">The result object to add.</param>
        /// <returns>This instance for fluent chaining.</returns>
        public MenuInputCommandInterpreterResult AddResult(object? result)
        {
            return AddResult(new MenuItemRunResult
            {
                Success = true,
                Result = result,
            });
        }

        /// <summary>
        /// Adds an input command result to the collection.
        /// </summary>
        /// <param name="result">The input command result to add.</param>
        /// <returns>This instance for fluent chaining.</returns>
        public MenuInputCommandInterpreterResult AddResult(IInputCommandResult? result)
        {
            menuItemRunResults.Add(result!);
            return this;
        }
    }
}
