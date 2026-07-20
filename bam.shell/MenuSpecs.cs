using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Describes a menu container and its associated item attribute types, used for scanning and creating menus.
    /// </summary>
    public class MenuSpecs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpecs"/> class with no item attribute types.
        /// </summary>
        /// <param name="containerType">The type that contains menu item methods.</param>
        public MenuSpecs(Type containerType) : this(containerType, new Type[] { })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpecs"/> class with the specified item attribute types.
        /// </summary>
        /// <param name="containerType">The type that contains menu item methods.</param>
        /// <param name="itemAttributeTypes">The attribute types used to identify menu items.</param>
        public MenuSpecs(Type containerType, params Type[] itemAttributeTypes)
        {
            this.ContainerType = containerType;
            this.ItemAttributeTypes = new HashSet<Type>(itemAttributeTypes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpecs"/> class with the specified item attribute types.
        /// </summary>
        /// <param name="containerType">The type that contains menu item methods.</param>
        /// <param name="itemAttributeTypes">The attribute types used to identify menu items.</param>
        public MenuSpecs(Type containerType, IEnumerable<Type> itemAttributeTypes)
        {
            this.ContainerType = containerType;
            this.ItemAttributeTypes = new HashSet<Type>(itemAttributeTypes);
        }

        /// <summary>
        /// Gets the type that contains menu item methods.
        /// </summary>
        public Type ContainerType { get; protected set; }

        /// <summary>
        /// Gets the set of attribute types used to identify menu items.
        /// </summary>
        public HashSet<Type> ItemAttributeTypes { get; protected set; }

        /// <summary>
        /// Gets the first <see cref="MenuSpec"/> from this specification, defaulting to <see cref="MenuItemAttribute"/> if no types are specified.
        /// </summary>
        /// <returns>A <see cref="MenuSpec"/> for the first item attribute type.</returns>
        public MenuSpec FirstSpec()
        {
            return new MenuSpec(ContainerType, ItemAttributeTypes.FirstOrDefault() ?? typeof(MenuItemAttribute));
        }

        /// <summary>
        /// Creates menus using the specified provider based on this specification.
        /// </summary>
        /// <param name="menuProvider">The provider used to create menu instances.</param>
        /// <returns>An enumerable of created menus.</returns>
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

        static IEnumerable<MenuSpecs> _menuSpecs = null!;
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

        /// <summary>
        /// Scans the specified assemblies for menu specifications.
        /// </summary>
        /// <param name="assemblies">The assemblies to scan.</param>
        /// <returns>An enumerable of discovered menu specifications.</returns>
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
        /// Scan the specified assembly for <see cref="MenuSpecs" />. Item attribute types are seeded from each
        /// container's <see cref="MenuAttribute.ItemAttributeType"/> (set by <see cref="MenuAttribute{TAttr}"/>,
        /// e.g. a console menu declaring its items as console commands), in addition to any
        /// <see cref="MenuItemAttribute"/>-decorated methods — so containers whose methods carry only the
        /// declared item attribute still yield a menu.
        /// </summary>
        /// <param name="assembly">The assembly to scan.</param>
        /// <returns>An enumerable of discovered menu specifications.</returns>
        public static IEnumerable<MenuSpecs> Scan(Assembly assembly)
        {
            Dictionary<Type, MenuSpecs> specsByContainer = new Dictionary<Type, MenuSpecs>();
            foreach(Type menuContainer in FindMenuTypes(assembly))
            {
                if(!specsByContainer.ContainsKey(menuContainer))
                {
                    specsByContainer.Add(menuContainer, new MenuSpecs(menuContainer));
                }
                MenuSpecs specs = specsByContainer[menuContainer];

                foreach(MenuAttribute menuAttribute in menuContainer.GetCustomAttributes<MenuAttribute>())
                {
                    if (menuAttribute.ItemAttributeType != null)
                    {
                        specs.AddItemAttributeType(menuAttribute.ItemAttributeType);
                    }
                }

                foreach(MethodInfo method in menuContainer.GetMethods())
                {
                    foreach(object attribute in method.GetCustomAttributes())
                    {
                        if (attribute is MenuItemAttribute)
                        {
                            specs.AddItemAttributeType(attribute.GetType());
                        }
                    }
                }
            }
            return specsByContainer.Values;
        }

        /// <summary>
        /// Finds all types in the specified assembly that are decorated with <see cref="MenuAttribute"/>.
        /// </summary>
        /// <param name="assembly">The assembly to scan.</param>
        /// <returns>An enumerable of types that are menu containers.</returns>
        public static IEnumerable<Type> FindMenuTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.HasCustomAttributeOfType<MenuAttribute>());
        }
    }
}
