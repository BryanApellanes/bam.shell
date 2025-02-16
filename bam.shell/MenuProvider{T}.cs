using System.Reflection;

namespace Bam.Shell
{
    public class MenuProvider<TAttr> : MenuProvider where TAttr : Attribute
    {
        public MenuProvider(IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner) : base(menuItemProvider, menuItemSelector, menuItemRunner)
        {
        }

        public override IMenu GetMenu(Type type)
        {
            return GetMenu<TAttr>(type);
        }

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
