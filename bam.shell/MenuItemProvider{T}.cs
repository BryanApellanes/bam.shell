using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// A strongly-typed menu item provider that discovers items by the specified attribute type.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
    public class MenuItemProvider<TAttr> : MenuItemProvider, IMenuItemProvider<TAttr> where TAttr : Attribute
    {
        /// <summary>
        /// Gets strongly-typed menu items from the specified container type.
        /// </summary>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <returns>An enumerable of strongly-typed menu items.</returns>
        public new IEnumerable<IMenuItem<TAttr>> GetMenuItems(Type containerType)
        {
            return GetMenuItems<TAttr>(containerType).Select(item => item);
        }

        /// <inheritdoc/>
        public override IEnumerable<IMenuItem> GetMenuItems(Type containerType, Type itemAttributeType)
        {
            foreach(MethodInfo method in containerType.GetMethods())
            {
                yield return new MenuItem(method, itemAttributeType);
            }
        }

        IEnumerable<IMenuItem> IMenuItemProvider.GetMenuItems(Type containerType)
        {
            return GetMenuItems(containerType);
        }
    }
}
