namespace Bam.Shell
{
    /// <summary>
    /// Provides data for input/output loop events such as starting, ending, and reading input.
    /// </summary>
    public class MenuInputOutputLoopEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuInputOutputLoopEventArgs"/> class.
        /// </summary>
        public MenuInputOutputLoopEventArgs() { }

        /// <summary>
        /// Gets or sets the input/output loop that raised the event.
        /// </summary>
        public IMenuInputOutputLoop MenuInputOutputLoop { get; set; }

        /// <summary>
        /// Gets or sets the input reader associated with the event.
        /// </summary>
        public IMenuInputReader MenuInputReader { get; set; }

        /// <summary>
        /// Gets or sets the menu input associated with the event.
        /// </summary>
        public IMenuInput MenuInput { get; set; }
    }
}
