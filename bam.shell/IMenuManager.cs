using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for managing menus, menu selection, rendering, and the input/output loop.
    /// </summary>
    public interface IMenuManager
    {
        /// <summary>
        /// Occurs when a menu item is selected.
        /// </summary>
        event EventHandler<MenuEventArgs> MenuItemSelected;

        /// <summary>
        /// Occurs before the menu manager state is updated.
        /// </summary>
        event EventHandler<MenuManagerUpdateStateEventArgs> StateUpdating;

        /// <summary>
        /// Occurs after the menu manager state has been updated.
        /// </summary>
        event EventHandler<MenuManagerUpdateStateEventArgs> StateUpdated;

        /// <summary>
        /// Occurs when two menus are registered with the same selector.
        /// </summary>
        event EventHandler<DuplicateMenuSelectorEventArgs> DuplicateMenuSelectorSpecified;

        /// <summary>
        /// Gets the currently selected menu.
        /// </summary>
        IMenu? CurrentMenu { get; }

        /// <summary>
        /// Gets the dictionary of menus indexed by their selector strings.
        /// </summary>
        Dictionary<string, IMenu> MenusBySelector { get; }

        /// <summary>
        /// Gets the list of all registered menus.
        /// </summary>
        IList<IMenu> Menus { get; }

        /// <summary>
        /// Subscribes the specified handler to the menu item selected event on all menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        void AddMenuItemSelectedHandler(EventHandler<MenuEventArgs> handler);

        /// <summary>
        /// Subscribes the specified handler to the menu item selection changed event on all menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        void AddMenuItemSelectionChangedHandler(EventHandler<MenuEventArgs> handler);

        /// <summary>
        /// Subscribes the specified handler to the menu item run started event on all menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        void AddMenuItemRunStartedHandler(EventHandler<MenuItemRunEventArgs> handler);

        /// <summary>
        /// Subscribes the specified handler to the menu item run complete event on all menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        void AddMenuItemRunCompleteHandler(EventHandler<MenuItemRunEventArgs> handler);

        /// <summary>
        /// Loads menus from the entry assembly and from <see cref="MenuSpecs.LoadList"/>.
        /// </summary>
        void LoadMenus();

        /// <summary>
        /// Loads menus by scanning the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for menu types.</param>
        void LoadMenus(Assembly assembly);

        /// <summary>
        /// Loads menus from the specified collection of menu specifications.
        /// </summary>
        /// <param name="menuSpecs">The menu specifications to create menus from.</param>
        void LoadMenus(IEnumerable<MenuSpecs> menuSpecs);

        /// <summary>
        /// Adds a menu to the manager.
        /// </summary>
        /// <param name="menu">The menu to add.</param>
        void AddMenu(IMenu menu);

        /// <summary>
        /// Creates and adds a menu for the specified container type.
        /// </summary>
        /// <param name="type">The container type to create a menu for.</param>
        /// <returns>The created menu, or null if the type does not define a menu.</returns>
        IMenu? AddMenu(Type type);

        /// <summary>
        /// Gets a menu by its selector string.
        /// </summary>
        /// <param name="selector">The selector string identifying the menu.</param>
        /// <returns>The matching menu, or null if not found.</returns>
        IMenu? GetMenu(string selector);

        /// <summary>
        /// Gets a menu by its container type.
        /// </summary>
        /// <param name="type">The container type to get the menu for.</param>
        /// <returns>The matching menu, or null if not found.</returns>
        IMenu? GetMenu(Type type);

        /// <summary>
        /// Re-renders the current menu based on the specified input.
        /// </summary>
        /// <param name="menuInput">The input that triggered the re-render.</param>
        void RerenderMenu(IMenuInput menuInput);

        /// <summary>
        /// Renders the current menu.
        /// </summary>
        void RenderMenu();

        /// <summary>
        /// Renders the specified menu.
        /// </summary>
        /// <param name="menu">The menu to render.</param>
        void RenderMenu(IMenu menu);

        /// <summary>
        /// Runs the currently selected menu item using the specified input.
        /// </summary>
        /// <param name="menuInput">The input to pass to the menu item.</param>
        /// <returns>The result of running the menu item.</returns>
        IMenuItemRunResult? RunMenuItem(IMenuInput menuInput);

        /// <summary>
        /// Runs the specified menu item with optional input.
        /// </summary>
        /// <param name="menuItem">The menu item to run.</param>
        /// <param name="menuInput">Optional input to pass to the menu item.</param>
        /// <returns>The result of running the menu item.</returns>
        IMenuItemRunResult? RunMenuItem(IMenuItem menuItem, IMenuInput? menuInput = null);

        /// <summary>
        /// Gets the currently selected menu item from the current menu.
        /// </summary>
        /// <returns>The selected menu item, or null if none is selected.</returns>
        IMenuItem? GetSelectedMenuItem();

        /// <summary>
        /// Selects the menu matching the specified selector.
        /// </summary>
        /// <param name="selector">The selector string identifying the menu.</param>
        /// <returns>The selected menu, or null if not found.</returns>
        IMenu? SelectMenu(string selector);

        /// <summary>
        /// Selects the specified menu as the current menu.
        /// </summary>
        /// <param name="menu">The menu to select.</param>
        /// <returns>The selected menu.</returns>
        IMenu? SelectMenu(IMenu menu);

        /// <summary>
        /// Selects the next menu in the list.
        /// </summary>
        /// <returns>The newly selected menu.</returns>
        IMenu? SelectNextMenu();

        /// <summary>
        /// Selects the previous menu in the list.
        /// </summary>
        /// <returns>The newly selected menu.</returns>
        IMenu? SelectPreviousMenu();

        /// <summary>
        /// Selects a menu item within the current menu based on user input.
        /// </summary>
        /// <param name="menuInput">The input identifying the menu item.</param>
        /// <returns>The selected menu item, or null if no match.</returns>
        IMenuItem? SelectMenuItem(IMenuInput menuInput);

        /// <summary>
        /// Selects a menu item within the specified menu based on user input.
        /// </summary>
        /// <param name="menu">The menu to select an item from.</param>
        /// <param name="menuInput">The input identifying the menu item.</param>
        /// <returns>The selected menu item, or null if no match.</returns>
        IMenuItem? SelectMenuItem(IMenu menu, IMenuInput menuInput);

        /// <summary>
        /// Loads all menus and starts the interactive input/output loop.
        /// </summary>
        /// <returns>This menu manager instance.</returns>
        IMenuManager StartInputOutputLoop();

    }
}