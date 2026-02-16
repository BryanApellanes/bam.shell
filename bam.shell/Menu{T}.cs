using Bam.Shell;

namespace Bam.Test.Menu
{
    /// <summary>
    /// A strongly-typed menu implementation whose items are identified by the specified attribute type.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
    public class Menu<TAttr> : Menu, IMenu<TAttr> where TAttr : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu{TAttr}"/> class.
        /// </summary>
        /// <param name="type">The container type that holds menu item methods.</param>
        /// <param name="menuItemProvider">The provider used to discover menu items.</param>
        /// <param name="menuItemSelector">The selector used to select menu items.</param>
        /// <param name="menuItemRunner">The runner used to execute menu items.</param>
        public Menu(Type type, IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner) : base(type, typeof(TAttr), menuItemProvider, menuItemSelector, menuItemRunner)
        {
            this.MenuItemProvider = new MenuItemProvider<TAttr>();
        }

        protected override IEnumerable<IMenuItem> GetItems()
        {
            IMenuItem<TAttr>[] items = this.MenuItemProvider.GetMenuItems(this.ContainerType).ToArray();
            if (items.Length > 0)
            {
                items[0].Selected = true;
            }
            return items;
        }

        /// <summary>
        /// Gets or sets the strongly-typed menu item provider.
        /// </summary>
        protected new IMenuItemProvider<TAttr> MenuItemProvider
        {
            get;
            set;
        }

        IEnumerable<IMenuItem<TAttr>> _items;

        /// <summary>
        /// Gets the collection of strongly-typed menu items, lazily initialized.
        /// </summary>
        public new IEnumerable<IMenuItem<TAttr>> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = this.GetItems().Select(item => (IMenuItem<TAttr>)item);
                    base.Items = _items;
                }
                return _items;
            }
        }

        protected static new string GetDisplayName(Type type)
        {
            Args.ThrowIfNull(type, nameof(type));

            string displayName = type.Name;
            if (type.HasCustomAttributeOfType(out TAttr attr))
            {
                if (attr.TryGetPropertyValue("DisplayName", type.Name, out string name))
                {
                    displayName = name;
                }            
            }

            return displayName;
        }

        protected static new string GetSelector(Type type)
        {
            Args.ThrowIfNull(type, nameof(type));

            string selector = type.Name;
            if(type.HasCustomAttributeOfType(out TAttr attr))
            {
                if(attr.TryGetPropertyValue("Selector", type.Name, out string s))
                {
                    selector = s;
                }
            }

            return selector;
        }

        protected static new string GetHeaderText(Type type)
        {
            Args.ThrowIfNull(type, nameof(type));

            string header = string.Empty;
            if(type.HasCustomAttributeOfType(out TAttr attr))
            {
                if(attr.TryGetPropertyValue("Header", string.Empty, out string text))
                {
                    header = text;
                }
            }

            return header;
        }

        protected static new string GetFooterText(Type type)
        {
            Args.ThrowIfNull(type, nameof(type));

            string footer = string.Empty;
            if (type.HasCustomAttributeOfType(out TAttr attr))
            {
                if (attr.TryGetPropertyValue("Footer", string.Empty, out string text))
                {
                    footer = text;
                }
            }

            return footer;
        }
    }
}
