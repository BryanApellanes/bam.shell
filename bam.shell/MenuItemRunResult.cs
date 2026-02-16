namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuItemRunResult"/> containing the result of running a menu item.
    /// </summary>
    public class MenuItemRunResult : IMenuItemRunResult
    {
        /// <summary>
        /// Gets or sets the result object returned by the menu item's method.
        /// </summary>
        public object? Result { get; set; } = null;

        /// <summary>
        /// Gets or sets the menu input that was used when running the item.
        /// </summary>
        public IMenuInput? MenuInput { get; set; }

        /// <summary>
        /// Gets or sets the menu item that was run.
        /// </summary>
        public IMenuItem? MenuItem
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the menu item executed successfully.
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        string? _message;

        /// <summary>
        /// Gets or sets the message describing the result or error. Falls back to the exception message if not explicitly set.
        /// </summary>
        public string? Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message) && Exception != null)
                {
                    _message = Exception.Message;
                }
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        /// <summary>
        /// Gets or sets the exception that occurred during execution, if any.
        /// </summary>
        public Exception? Exception
        {
            get;
            set;
        }
    }
}
