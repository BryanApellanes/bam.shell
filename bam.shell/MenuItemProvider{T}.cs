using System.Reflection;

namespace Bam.Shell
{
    public class MenuItemProvider<TAttr> : MenuItemProvider, IMenuItemProvider<TAttr> where TAttr : Attribute
    {
        public new IEnumerable<IMenuItem<TAttr>> GetMenuItems(Type containerType)
        {
            return GetMenuItems<TAttr>(containerType).Select(item => item);
        }

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
