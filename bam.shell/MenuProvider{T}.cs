using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// A strongly-typed menu provider that creates menus using the specified attribute type for item discovery.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
    public class MenuProvider<TAttr> : MenuProvider where TAttr : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProvider{TAttr}"/> class.
        /// </summary>
        /// <param name="menuItemProvider">The provider used to discover menu items.</param>
        /// <param name="menuItemSelector">The selector used to select menu items.</param>
        /// <param name="menuItemRunner">The runner used to execute menu items.</param>
        public MenuProvider(IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner) : base(menuItemProvider, menuItemSelector, menuItemRunner)
        {
        }

        /// <inheritdoc/>
        public override IMenu GetMenu(Type type)
        {
            return GetMenu<TAttr>(type);
        }

        /// <inheritdoc/>
        public override IEnumerable<IMenu> GetMenus(Assembly assembly)
        {
            if (assembly == null)
            {
                return new List<IMenu>();
            }

            return GetMenus<TAttr>(assembly);
        }
    }
}
