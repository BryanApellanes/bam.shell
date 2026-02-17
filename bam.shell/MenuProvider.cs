using Bam.Test.Menu;
using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuProvider"/> that creates and caches menu instances by selector.
    /// </summary>
    public class MenuProvider : IMenuProvider
    {
        private Dictionary<string, IMenu> menusBySelector = new Dictionary<string, IMenu>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProvider"/> class.
        /// </summary>
        /// <param name="menuItemProvider">The provider used to discover menu items.</param>
        /// <param name="menuItemSelector">The selector used to select menu items.</param>
        /// <param name="menuItemRunner">The runner used to execute menu items.</param>
        public MenuProvider(IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner)
        {
            this.MenuItemRunner = menuItemRunner;
            this.MenuItemProvider = menuItemProvider;
            this.MenuItemSelector = menuItemSelector;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProvider"/> class with a default <see cref="MenuItemSelector"/>.
        /// </summary>
        /// <param name="menuItemProvider">The provider used to discover menu items.</param>
        /// <param name="menuItemRunner">The runner used to execute menu items.</param>
        public MenuProvider(IMenuItemProvider menuItemProvider, IMenuItemRunner menuItemRunner) : this(menuItemProvider, new MenuItemSelector(), menuItemRunner)
        {
        }

        /// <summary>
        /// Occurs when two menus are registered with the same selector.
        /// </summary>
        public event EventHandler<DuplicateMenuSelectorEventArgs> DuplicateMenuSelectorSpecified = null!;
        private void AddMenu(IMenu menu)
        {
            if (menusBySelector.ContainsKey(menu.Selector))
            {
                DuplicateMenuSelectorSpecified?.Invoke(this, new DuplicateMenuSelectorEventArgs
                {
                    FirstMenu = menusBySelector[menu.Selector],
                    SecondMenu = menu
                });

                menusBySelector[menu.Selector] = menu;
            }
            else
            {
                menusBySelector.Add(menu.Selector, menu);
            }
        }

        /// <summary>
        /// Gets or sets the runner used to execute menu items.
        /// </summary>
        protected IMenuItemRunner MenuItemRunner
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the provider used to discover menu items.
        /// </summary>
        protected IMenuItemProvider MenuItemProvider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selector used to select menu items.
        /// </summary>
        protected IMenuItemSelector MenuItemSelector
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a menu for the specified container type using the default <see cref="MenuItemAttribute"/>.
        /// </summary>
        /// <param name="containerType">The container type to get the menu for.</param>
        /// <returns>The menu for the specified type.</returns>
        public virtual IMenu GetMenu(Type containerType)
        {
            return GetMenu<MenuItemAttribute>(containerType);
        }

        /// <summary>
        /// Determines whether a menu with the specified selector exists.
        /// </summary>
        /// <param name="selector">The selector to check.</param>
        /// <returns>True if a menu with the selector exists; otherwise, false.</returns>
        public bool HasMenu(string selector)
        {
            return menusBySelector.ContainsKey(selector);
        }

        /// <summary>
        /// Gets a menu by its selector string.
        /// </summary>
        /// <param name="selector">The selector string identifying the menu.</param>
        /// <returns>The matching menu, or null if not found.</returns>
        public IMenu GetMenu(string selector)
        {
            if (menusBySelector.ContainsKey(selector))
            {
                return menusBySelector[selector];
            }

            return null!;
        }

        /// <summary>
        /// Gets a strongly-typed menu for the specified container type.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
        /// <param name="containerType">The container type to get the menu for.</param>
        /// <returns>The strongly-typed menu.</returns>
        public IMenu<TAttr> GetMenu<TAttr>(Type containerType) where TAttr : Attribute
        {
            Args.ThrowIfNull(containerType, "type");

            return CreateMenu<TAttr>(containerType);
        }

        /// <summary>
        /// Creates a strongly-typed menu for the specified container type.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
        /// <param name="containerType">The container type to create the menu for.</param>
        /// <returns>The created strongly-typed menu.</returns>
        public IMenu<TAttr> CreateMenu<TAttr>(Type containerType) where TAttr : Attribute
        {
            Menu<TAttr> menu = new Menu<TAttr>(containerType, this.MenuItemProvider, this.MenuItemSelector, this.MenuItemRunner);

            AddMenu(menu);

            return menu;
        }

        /// <summary>
        /// Creates menus from the specified menu specifications.
        /// </summary>
        /// <param name="menuSpec">The menu specifications to create menus from.</param>
        /// <returns>An enumerable of created menus.</returns>
        public IEnumerable<IMenu> CreateMenus(MenuSpecs menuSpec)
        {
            foreach(Type itemAttributeType in menuSpec.ItemAttributeTypes)
            {
                yield return CreateMenu(menuSpec.ContainerType, itemAttributeType);
            }
        }

        /// <summary>
        /// Creates a menu from the specified menu specification.
        /// </summary>
        /// <param name="menuSpec">The menu specification to create a menu from.</param>
        /// <returns>The created menu.</returns>
        public IMenu CreateMenu(MenuSpec menuSpec)
        {
            return CreateMenu(menuSpec.ContainerType, menuSpec.ItemAttributeType);
        }

        /// <summary>
        /// Creates a menu for the specified container type and item attribute type.
        /// </summary>
        /// <param name="containerType">The type containing menu item methods.</param>
        /// <param name="itemAttributeType">The attribute type identifying menu items.</param>
        /// <returns>The created menu.</returns>
        public IMenu CreateMenu(Type containerType, Type itemAttributeType)
        {
            Menu menu = new Menu(containerType, itemAttributeType, this.MenuItemProvider, this.MenuItemSelector, this.MenuItemRunner);

            AddMenu(menu);

            return menu;
        }

        /// <summary>
        /// Gets the default menu, which is the first menu discovered from the entry assembly.
        /// </summary>
        /// <returns>The default menu, or null if none exists.</returns>
        public IMenu? GetDefaultMenu()
        {
            return GetMenus().FirstOrDefault();
        }

        /// <summary>
        /// Gets all menus from the entry assembly.
        /// </summary>
        /// <returns>An enumerable of all discovered menus.</returns>
        public IEnumerable<IMenu> GetMenus()
        {
            return GetMenus(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Gets all menus from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for menus.</param>
        /// <returns>An enumerable of all discovered menus, or an empty list if the assembly is null.</returns>
        public virtual IEnumerable<IMenu> GetMenus(Assembly? assembly)
        {
            if (assembly == null)
            {
                return new List<IMenu>();
            }

            return GetMenus<MenuItemAttribute>(assembly);
        }

        /// <summary>
        /// Gets all strongly-typed menus from the specified assembly.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
        /// <param name="assembly">The assembly to scan for menus.</param>
        /// <returns>An enumerable of strongly-typed menus.</returns>
        public IEnumerable<IMenu<TAttr>> GetMenus<TAttr>(Assembly assembly) where TAttr : Attribute
        {
            foreach (Type type in assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<MenuAttribute>()))
            {
                yield return GetMenu<TAttr>(type);
            }
        }
    }
}
