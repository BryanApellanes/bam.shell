namespace Bam.Shell
{
    /// <summary>
    /// Provides data for menu item run events, including the menu, item, input, and result.
    /// </summary>
    public class MenuItemRunEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemRunEventArgs"/> class.
        /// </summary>
        public MenuItemRunEventArgs() { }

        /// <summary>
        /// Gets or sets the menu containing the item that was run.
        /// </summary>
        public IMenu Menu { get; set; }

        /// <summary>
        /// Gets or sets the menu item that was run.
        /// </summary>
        public IMenuItem MenuItem { get; set; }

        /// <summary>
        /// Gets or sets the input that was provided when running the menu item.
        /// </summary>
        public IMenuInput MenuInput { get; set; }

        /// <summary>
        /// Gets or sets the result of running the menu item.
        /// </summary>
        public IMenuItemRunResult Result { get; set; }
    }
}
