using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuItemProvider"/> that discovers menu items by scanning for attribute-decorated methods.
    /// </summary>
    public class MenuItemProvider : IMenuItemProvider
    {
        /// <summary>
        /// Gets menu items decorated with <see cref="MenuItemAttribute"/> from the specified container type.
        /// </summary>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <returns>An enumerable of strongly-typed menu items.</returns>
        public IEnumerable<IMenuItem<MenuItemAttribute>> GetMenuItems(Type containerType)
        {
            return GetMenuItems<MenuItemAttribute>(containerType);
        }

        /// <summary>
        /// Gets menu items from the specified instance's type, assigning the instance for method invocation.
        /// </summary>
        /// <param name="instance">The instance to discover menu items from.</param>
        /// <returns>An enumerable of menu items with the instance assigned.</returns>
        public virtual IEnumerable<IMenuItem> GetMenuItems(object instance)
        {
            foreach(IMenuItem item in GetMenuItems<MenuItemAttribute>(instance.GetType()))
            {
                item.Instance = instance;
                yield return item;
            }
        }

        /// <summary>
        /// Gets strongly-typed menu items from the specified container type.
        /// </summary>
        /// <typeparam name="TAttr">The attribute type identifying menu items.</typeparam>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <returns>An enumerable of strongly-typed menu items.</returns>
        public virtual IEnumerable<IMenuItem<TAttr>> GetMenuItems<TAttr>(Type containerType) where TAttr : Attribute
        {
            foreach(MethodInfo method in containerType.GetMethods())
            {
                if (method.HasCustomAttributeOfType(out TAttr attribute))
                {
                    yield return new MenuItem<TAttr>(method);
                }
            }
        }

        /// <summary>
        /// Gets menu items from the specified container type using the specified item attribute type.
        /// </summary>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <param name="itemAttributeType">The attribute type identifying menu items.</param>
        /// <returns>An enumerable of menu items.</returns>
        public virtual IEnumerable<IMenuItem> GetMenuItems(Type containerType, Type itemAttributeType)
        {
            foreach(MethodInfo method in containerType.GetMethods())
            {
                if(method.HasCustomAttributeOfType(itemAttributeType, out object attribute))
                {
                    yield return new MenuItem(method, itemAttributeType)
                    {
                        Attribute = (Attribute)attribute
                    };
                }
            }
        }

        IEnumerable<IMenuItem> IMenuItemProvider.GetMenuItems(Type containterType)
        {
            return GetMenuItems(containterType);
        }
    }
}
