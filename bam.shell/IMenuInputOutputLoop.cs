namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for the interactive input/output loop that drives menu interaction.
    /// </summary>
    public interface IMenuInputOutputLoop
    {
        /// <summary>
        /// Occurs when the loop is starting.
        /// </summary>
        event EventHandler<MenuInputOutputLoopEventArgs> Starting;

        /// <summary>
        /// Occurs when reading input has completed.
        /// </summary>
        event EventHandler<MenuInputOutputLoopEventArgs> ReadingInputComplete;

        /// <summary>
        /// Occurs when input is being processed.
        /// </summary>
        event EventHandler<MenuInputOutputLoopEventArgs> ProcessingInput;

        /// <summary>
        /// Occurs when the loop is ending.
        /// </summary>
        event EventHandler<MenuInputOutputLoopEventArgs> Ending;

        /// <summary>
        /// Occurs when a menu item begins executing.
        /// </summary>
        event EventHandler<MenuItemRunEventArgs> MenuItemRunStarted;

        /// <summary>
        /// Occurs when a menu item has finished executing.
        /// </summary>
        event EventHandler<MenuItemRunEventArgs> MenuItemRunComplete;

        /// <summary>
        /// Gets the menu manager that owns this loop.
        /// </summary>
        IMenuManager MenuManager { get; }

        /// <summary>
        /// Starts the input/output loop using the default input reader.
        /// </summary>
        void Start();

        /// <summary>
        /// Starts the input/output loop using the specified input reader.
        /// </summary>
        /// <param name="menuInputReader">The input reader to use.</param>
        void Start(IMenuInputReader menuInputReader);

        /// <summary>
        /// Ends the input/output loop using the default input reader.
        /// </summary>
        /// <returns>The menu manager.</returns>
        IMenuManager End();

        /// <summary>
        /// Ends the input/output loop using the specified input reader.
        /// </summary>
        /// <param name="menuInputReader">The input reader to include in the ending event.</param>
        /// <returns>The menu manager.</returns>
        IMenuManager End(IMenuInputReader menuInputReader);
    }
}
