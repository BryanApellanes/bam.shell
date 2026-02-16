using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for creating and providing menu instances.
    /// </summary>
    public interface IMenuProvider
    {
        /// <summary>
        /// Occurs when two menus are registered with the same selector.
        /// </summary>
        event EventHandler<DuplicateMenuSelectorEventArgs> DuplicateMenuSelectorSpecified;

        /// <summary>
        /// Gets the default menu, typically the first menu discovered.
        /// </summary>
        /// <returns>The default menu, or null if none exists.</returns>
        IMenu? GetDefaultMenu();

        /// <summary>
        /// Gets all menus from the entry assembly.
        /// </summary>
        /// <returns>An enumerable of all discovered menus.</returns>
        IEnumerable<IMenu> GetMenus();

        /// <summary>
        /// Gets all menus from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for menus.</param>
        /// <returns>An enumerable of all discovered menus.</returns>
        IEnumerable<IMenu> GetMenus(Assembly assembly);

        /// <summary>
        /// Gets all strongly-typed menus from the specified assembly.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
        /// <param name="assembly">The assembly to scan for menus.</param>
        /// <returns>An enumerable of strongly-typed menus.</returns>
        IEnumerable<IMenu<TAttr>> GetMenus<TAttr>(Assembly assembly) where TAttr : Attribute;

        /// <summary>
        /// Gets a menu for the specified container type.
        /// </summary>
        /// <param name="type">The container type to get the menu for.</param>
        /// <returns>The menu for the specified type.</returns>
        IMenu GetMenu(Type type);

        /// <summary>
        /// Determines whether a menu with the specified selector exists.
        /// </summary>
        /// <param name="selector">The selector to check.</param>
        /// <returns>True if a menu with the selector exists; otherwise, false.</returns>
        bool HasMenu(string selector);

        /// <summary>
        /// Gets a menu by its selector string.
        /// </summary>
        /// <param name="selector">The selector string identifying the menu.</param>
        /// <returns>The matching menu.</returns>
        IMenu GetMenu(string selector);

        /// <summary>
        /// Gets a strongly-typed menu for the specified container type.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
        /// <param name="containerType">The container type to get the menu for.</param>
        /// <returns>The strongly-typed menu.</returns>
        IMenu<TAttr> GetMenu<TAttr>(Type containerType) where TAttr : Attribute;

        /// <summary>
        /// Creates a strongly-typed menu for the specified container type.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
        /// <param name="containerType">The container type to create the menu for.</param>
        /// <returns>The created strongly-typed menu.</returns>
        IMenu<TAttr> CreateMenu<TAttr>(Type containerType) where TAttr : Attribute;

        /// <summary>
        /// Creates menus from the specified menu specifications.
        /// </summary>
        /// <param name="menuSpec">The menu specifications to create menus from.</param>
        /// <returns>An enumerable of created menus.</returns>
        IEnumerable<IMenu> CreateMenus(MenuSpecs menuSpec);

        /// <summary>
        /// Creates a menu for the specified container type and item attribute type.
        /// </summary>
        /// <param name="containerType">The type containing menu item methods.</param>
        /// <param name="itemAttributeType">The attribute type identifying menu items.</param>
        /// <returns>The created menu.</returns>
        IMenu CreateMenu(Type containerType, Type itemAttributeType);
    }
}
