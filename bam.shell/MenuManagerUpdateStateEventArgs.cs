namespace Bam.Shell
{
    /// <summary>
    /// Provides data for menu manager state update events.
    /// </summary>
    public class MenuManagerUpdateStateEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuManagerUpdateStateEventArgs"/> class.
        /// </summary>
        public MenuManagerUpdateStateEventArgs() { }

        /// <summary>
        /// Gets or sets the menu associated with the state update.
        /// </summary>
        public IMenu Menu { get; set; } = null!;

        /// <summary>
        /// Gets or sets the input that triggered the state update.
        /// </summary>
        public IMenuInput MenuInput { get; set; } = null!;

        /// <summary>
        /// Gets the currently selected menu item from the menu, if any.
        /// </summary>
        public IMenuItem? SelectedMenuItem
        {
            get
            {
                return Menu?.SelectedItem ?? null;
            }
        }
    }
}
