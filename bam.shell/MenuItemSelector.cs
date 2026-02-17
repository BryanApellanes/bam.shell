namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuItemSelector"/> that selects menu items based on navigation keys, item numbers, or selectors.
    /// </summary>
    public class MenuItemSelector : IMenuItemSelector
    {
        /// <summary>
        /// Occurs when the selected menu item changes.
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuItemSelectionChanged = null!;

        /// <summary>
        /// Selects a menu item within the specified menu based on the given input.
        /// </summary>
        /// <param name="menu">The menu to select an item from.</param>
        /// <param name="menuInput">The input identifying which item to select.</param>
        /// <returns>The selected menu item, or null if no match was found.</returns>
        public virtual IMenuItem? SelectMenuItem(IMenu menu, IMenuInput menuInput)
        {
            IMenuItem? currentSelection = menu.SelectedItem;
            IMenuItem? newSelection = null;
            if (menuInput.NextItem)
            {
                newSelection = menu.SelectNextItem();
            }
            else if (menuInput.PreviousItem)
            {
                newSelection = menu.SelectPreviousItem();
            }
            else if (menuInput.ItemNumber > -1)
            {
                newSelection = menu.SelectItemNumber(menuInput.ItemNumber);
                if(newSelection != null)
                {
                    menuInput.Input.Clear();
                }
            }
            else if (menuInput.IsSelector && !string.IsNullOrEmpty(menuInput.Selector))
            {
                newSelection = menu.SelectItem(menuInput.Selector);
                if(newSelection != null)
                {
                    menuInput.Input.Clear();
                }
            }

            if (currentSelection != null)
            {
                if (newSelection != null && currentSelection != newSelection)
                {
                    MenuItemSelectionChanged?.Invoke(this, new MenuEventArgs
                    {
                        Menu = menu,
                        PreviousMenuItem = currentSelection,
                        MenuItem = newSelection,
                    });
                }
            }
            return newSelection;
        }
    }
}
