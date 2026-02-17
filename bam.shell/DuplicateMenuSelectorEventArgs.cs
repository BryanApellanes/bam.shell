namespace Bam.Shell
{
    /// <summary>
    /// Provides data for the event raised when two menus are registered with the same selector.
    /// </summary>
    public class DuplicateMenuSelectorEventArgs
    {
        /// <summary>
        /// Gets or sets the first menu registered with the duplicate selector.
        /// </summary>
        public IMenu FirstMenu { get; set; } = null!;

        /// <summary>
        /// Gets or sets the second menu registered with the duplicate selector.
        /// </summary>
        public IMenu SecondMenu { get; set;} = null!;
    }
}
