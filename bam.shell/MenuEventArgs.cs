namespace Bam.Shell
{
    /// <summary>
    /// Provides data for menu-related events such as item selection changes.
    /// </summary>
    public class MenuEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuEventArgs"/> class.
        /// </summary>
        public MenuEventArgs() { }

        /// <summary>
        /// Gets or sets the menu associated with the event.
        /// </summary>
        public IMenu Menu { get; set; } = null!;

        /// <summary>
        /// Gets or sets the previously selected menu item, if applicable.
        /// </summary>
        public IMenuItem? PreviousMenuItem { get; set; }

        /// <summary>
        /// Gets or sets the currently selected menu item.
        /// </summary>
        public IMenuItem? MenuItem { get; set; }
    }
}
