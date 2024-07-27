using Bam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class MenuSpecs
    {
        public MenuSpecs(Type containerType) : this(containerType, new Type[] { })
        {
        }

        public MenuSpecs(Type containerType, params Type[] itemAttributeTypes)
        {
            this.ContainerType = containerType;
            this.ItemAttributeTypes = new HashSet<Type>(itemAttributeTypes);
        }

        public MenuSpecs(Type containerType, IEnumerable<Type> itemAttributeTypes)
        {
            this.ContainerType = containerType;
            this.ItemAttributeTypes = new HashSet<Type>(itemAttributeTypes);
        }

        public Type ContainerType { get; protected set; }

        public HashSet<Type> ItemAttributeTypes { get; protected set; }

        public MenuSpec FirstSpec()
        {
            return new MenuSpec(ContainerType, ItemAttributeTypes.FirstOrDefault() ?? typeof(MenuItemAttribute));
        }

        public IEnumerable<IMenu> CreateMenus(IMenuProvider menuProvider)
        {
            Args.ThrowIfNull(menuProvider, nameof(menuProvider));

            return menuProvider.CreateMenus(this);
        }

        protected MenuSpecs AddItemAttributeType(Type type)
        {
            ItemAttributeTypes.Add(type);
            return this;
        }

        static IEnumerable<MenuSpecs> _menuSpecs;
        static readonly object _menuSpecLock = new object();
        /// <summary>
        /// Gets or sets a list of <see cref="MenuSpecs" /> to load.
        /// </summary>
        public static IEnumerable<MenuSpecs> LoadList
        {
            get => _menuSpecLock.DoubleCheckLock(ref _menuSpecs, LoadMenuSpecs);
            set => _menuSpecs = value.ToList();
        }

        protected static IEnumerable<MenuSpecs> LoadMenuSpecs()
        {
            Assembly? entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                return LoadMenuSpecs(entryAssembly);
            }
            return new List<MenuSpecs>();
        }

        protected static IEnumerable<MenuSpecs> LoadMenuSpecs(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (MenuSpecs menuSpec in Scan(assembly))
                {
                    yield return menuSpec;
                }
            }
        }

        public static IEnumerable<MenuSpecs> Scan(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (MenuSpecs spec in Scan(assembly))
                {
                    yield return spec;
                }
            }
        }

        /// <summary>
        /// Scan the specified assembly for <see cref="MenuSpecs" />.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<MenuSpecs> Scan(Assembly assembly)
        {
            Dictionary<Type, MenuSpecs> specsByContainer = new Dictionary<Type, MenuSpecs>();
            foreach(Type menuContainer in FindMenuTypes(assembly))
            {
                foreach(MethodInfo method in menuContainer.GetMethods())
                {
                    if(!specsByContainer.ContainsKey(menuContainer))
                    {
                        specsByContainer.Add(menuContainer, new MenuSpecs(menuContainer));
                    }

                    foreach(object attribute in method.GetCustomAttributes())
                    {
                        if (attribute is MenuItemAttribute)
                        {
                            specsByContainer[menuContainer].AddItemAttributeType(attribute.GetType());
                        }
                    }
                }
            }
            return specsByContainer.Values;
        }

        public static IEnumerable<Type> FindMenuTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.HasCustomAttributeOfType<MenuAttribute>());
        }
    }
}
