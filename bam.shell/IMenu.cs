namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for a menu containing selectable items that can be navigated and executed.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Occurs when a menu item is selected.
        /// </summary>
        event EventHandler<MenuEventArgs> MenuItemSelected;

        /// <summary>
        /// Occurs when the selected menu item changes.
        /// </summary>
        event EventHandler<MenuEventArgs> MenuItemSelectionChanged;

        /// <summary>
        /// Occurs when a menu item begins executing.
        /// </summary>
        event EventHandler<MenuItemRunEventArgs> MenuItemRunStarted;

        /// <summary>
        /// Occurs when a menu item has finished executing.
        /// </summary>
        event EventHandler<MenuItemRunEventArgs> MenuItemRunComplete;

        /// <summary>
        /// Gets or sets the type that contains the menu item methods.
        /// </summary>
        Type ContainerType { get; set; }

        /// <summary>
        /// Gets or sets the attribute type used to identify menu items.
        /// </summary>
        Type ItemAttributeType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this menu is currently selected.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// Gets the name of the menu.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the display name shown to the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the selector string used to navigate to this menu.
        /// </summary>
        string Selector { get; }

        /// <summary>
        /// Gets the header text displayed above the menu items.
        /// </summary>
        string HeaderText { get; }

        /// <summary>
        /// Gets the footer text displayed below the menu items.
        /// </summary>
        string FooterText { get; }

        /// <summary>
        /// Gets the console key used to exit the menu.
        /// </summary>
        ConsoleKey ExitKey { get; }

        /// <summary>
        /// Gets the currently selected menu item.
        /// </summary>
        IMenuItem? SelectedItem { get; }

        /// <summary>
        /// Gets the index of the currently selected item.
        /// </summary>
        int SelectedItemIndex { get; }

        /// <summary>
        /// Gets the collection of menu items.
        /// </summary>
        IEnumerable<IMenuItem> Items { get; }

        /// <summary>
        /// Gets the <see cref="MenuSpec"/> describing this menu's container and item attribute types.
        /// </summary>
        /// <returns>A <see cref="MenuSpec"/> for this menu.</returns>
        MenuSpec GetSpec();

        /// <summary>
        /// Unselects all menu items.
        /// </summary>
        void UnselectAll();

        /// <summary>
        /// Gets a menu item by its selector string.
        /// </summary>
        /// <param name="selector">The selector string of the item.</param>
        /// <returns>The matching menu item, or null if not found.</returns>
        IMenuItem? GetItem(string selector);

        /// <summary>
        /// Selects a menu item based on the specified input.
        /// </summary>
        /// <param name="menuInput">The input identifying the item to select.</param>
        /// <returns>The selected menu item, or null if no match.</returns>
        IMenuItem? SelectItem(IMenuInput menuInput);

        /// <summary>
        /// Selects a menu item by its selector string.
        /// </summary>
        /// <param name="itemSelector">The selector string of the item to select.</param>
        /// <returns>The selected menu item, or null if not found.</returns>
        IMenuItem? SelectItem(string itemSelector);

        /// <summary>
        /// Selects a menu item by its zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to select.</param>
        /// <returns>The selected menu item, or null if the index is out of range.</returns>
        IMenuItem? SelectItem(int index);

        /// <summary>
        /// Selects a menu item by its one-based item number.
        /// </summary>
        /// <param name="itemNumber">The one-based number of the item to select.</param>
        /// <returns>The selected menu item, or null if the number is out of range.</returns>
        IMenuItem? SelectItemNumber(int itemNumber);

        /// <summary>
        /// Selects the next menu item in the list.
        /// </summary>
        /// <returns>The newly selected menu item, or null if at the end of the list.</returns>
        IMenuItem? SelectNextItem();

        /// <summary>
        /// Selects the previous menu item in the list.
        /// </summary>
        /// <returns>The newly selected menu item, or null if at the beginning of the list.</returns>
        IMenuItem? SelectPreviousItem();

        /// <summary>
        /// Runs the currently selected menu item with the specified input.
        /// </summary>
        /// <param name="menuInput">The input to pass to the menu item.</param>
        /// <returns>The result of running the menu item.</returns>
        IMenuItemRunResult RunItem(IMenuInput menuInput);

        /// <summary>
        /// Runs the specified menu item with optional input.
        /// </summary>
        /// <param name="menuItem">The menu item to run.</param>
        /// <param name="menuInput">Optional input to pass to the menu item.</param>
        /// <returns>The result of running the menu item.</returns>
        IMenuItemRunResult RunItem(IMenuItem menuItem, IMenuInput? menuInput = null);
    }
}
