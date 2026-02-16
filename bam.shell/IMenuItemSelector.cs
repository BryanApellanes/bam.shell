namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for selecting menu items within a menu based on user input.
    /// </summary>
    public interface IMenuItemSelector
    {
        /// <summary>
        /// Occurs when the selected menu item changes.
        /// </summary>
        event EventHandler<MenuEventArgs> MenuItemSelectionChanged;

        /// <summary>
        /// Selects a menu item within the specified menu based on the given input.
        /// </summary>
        /// <param name="menu">The menu to select an item from.</param>
        /// <param name="menuInput">The input identifying which item to select.</param>
        /// <returns>The selected menu item, or null if no match was found.</returns>
        IMenuItem? SelectMenuItem(IMenu menu, IMenuInput menuInput);
    }
}
