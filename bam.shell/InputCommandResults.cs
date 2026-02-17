namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IInputCommandResults"/> that collects input command results.
    /// </summary>
    public class InputCommandResults : IInputCommandResults
    {
        List<IInputCommandResult> _results;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCommandResults"/> class.
        /// </summary>
        public InputCommandResults()
        {
            _results = new List<IInputCommandResult>();
        }

        /// <summary>
        /// Gets a value indicating whether all commands executed successfully (no exceptions occurred).
        /// </summary>
        public bool Sucess
        {
            get
            {
                return Exception == null;
            }
        }

        /// <summary>
        /// Gets or sets the overall message for the results.
        /// </summary>
        public string Message { get; set; } = null!;

        Exception? _exception;

        /// <summary>
        /// Gets or sets the aggregate exception from all results. If not explicitly set, aggregates exceptions from individual results.
        /// </summary>
        public Exception Exception
        {
            get
            {
                if( _exception == null)
                {
                    if(Results != null)
                    {
                        IEnumerable<Exception> exceptions = Results.Select(r => r.Exception);
                        if(exceptions.Any())
                        {
                            _exception = new AggregateException(exceptions);
                        }
                    }
                }
                return _exception!;
            }
            set
            {
                _exception = value;
            }
        }

        /// <summary>
        /// Gets the collection of individual input command results.
        /// </summary>
        public IEnumerable<IInputCommandResult> Results
        {
            get
            {
                return _results;
            }
        }

        /// <summary>
        /// Adds an input command result to the collection.
        /// </summary>
        /// <param name="result">The result to add.</param>
        public void AddResult(IInputCommandResult result)
        {
            _results.Add(result);
        }
    }
}
