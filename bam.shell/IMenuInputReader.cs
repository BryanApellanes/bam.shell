namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for reading user input for the menu system.
    /// </summary>
    public interface IMenuInputReader
    {
        /// <summary>
        /// Occurs when input is being read.
        /// </summary>
        event EventHandler<MenuInputOutputLoopEventArgs> ReadingInput;

        /// <summary>
        /// Reads and returns the next menu input from the user.
        /// </summary>
        /// <returns>The menu input read from the user.</returns>
        IMenuInput ReadMenuInput();
    }
}
