using Bam.Shell;

namespace Bam.Test.Menu
{
    /// <summary>
    /// Default implementation of <see cref="IMenu"/> that discovers menu items from a container type using attribute-decorated methods.
    /// </summary>
    public class Menu : IMenu
    {
        /// <summary>
        /// The default header text displayed above menu items.
        /// </summary>
        public const string DefaultHeaderText = "Select an option below:";

        /// <summary>
        /// The default footer text displayed below menu items.
        /// </summary>
        public const string DefaultFooterText = "-----------------------------------";

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="type">The container type that holds menu item methods.</param>
        /// <param name="itemAttributeType">The attribute type used to identify menu items.</param>
        /// <param name="menuItemProvider">The provider used to discover menu items.</param>
        /// <param name="menuItemSelector">The selector used to select menu items.</param>
        /// <param name="menuItemRunner">The runner used to execute menu items.</param>
        public Menu(Type type, Type itemAttributeType, IMenuItemProvider menuItemProvider, IMenuItemSelector menuItemSelector, IMenuItemRunner menuItemRunner)
        {
            this.ContainerType = type;
            this.ItemAttributeType = itemAttributeType;
            this.Name = GetName();
            this.DisplayName = GetDisplayName();
            this.Description = GetDescription();
            this.Selector = GetSelector();
            this.HeaderText = GetHeaderText();
            this.FooterText = GetFooterText();
            this.MenuItemSelector = menuItemSelector;
            this.MenuItemRunner = menuItemRunner;
            this.MenuItemProvider = menuItemProvider;            
            this.ExitKey = MenuManager.ExitKey;

            this.MenuItemSelector.MenuItemSelectionChanged += (sender, args) => this.MenuItemSelectionChanged?.Invoke(sender, args);
        }
        
        /// <summary>
        /// Occurs when a menu item is selected.
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuItemSelected = null!;

        /// <summary>
        /// Occurs when the selected menu item changes.
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuItemSelectionChanged = null!;

        /// <summary>
        /// Occurs when a menu item begins executing.
        /// </summary>
        public event EventHandler<MenuItemRunEventArgs> MenuItemRunStarted = null!;

        /// <summary>
        /// Occurs when a menu item has finished executing.
        /// </summary>
        public event EventHandler<MenuItemRunEventArgs> MenuItemRunComplete = null!;

        /// <summary>
        /// Gets the menu item selector used to handle item selection.
        /// </summary>
        public IMenuItemSelector MenuItemSelector
        {
            get;
            private set;
        }

        protected virtual IEnumerable<IMenuItem> GetItems()
        {
            IMenuItem[] items = this.MenuItemProvider.GetMenuItems(this.ContainerType, this.ItemAttributeType).ToArray();
            if (items.Length > 0)
            {
                items[0].Selected = true;
            }
            return items;
        }

        protected virtual string GetName()
        {
            return GetMenuAttributeProperty(nameof(Name));
        }

        protected virtual string GetDisplayName()
        {
            return GetMenuAttributeProperty(nameof(DisplayName));
        }

        protected virtual string GetDescription()
        {
            return GetMenuAttributeProperty(nameof(Description));
        }

        protected virtual string GetSelector()
        {
            return GetMenuAttributeProperty(nameof(Selector), GetName().PascalCase(true, " ").CaseAcronym().ToLowerInvariant());
        }

        protected virtual string GetHeaderText()
        {
            return GetMenuAttributeProperty(nameof(HeaderText), DefaultHeaderText);
        }

        protected virtual string GetFooterText()
        {
            return GetMenuAttributeProperty(nameof(FooterText), DefaultFooterText);
        }

        protected string GetMenuAttributeProperty(string propertyName)
        {
            return GetMenuAttributeProperty(this.ContainerType, this.ItemAttributeType, propertyName);
        }

        protected string GetMenuAttributeProperty(string propertyName, string valueIfPropertyMissing)
        {
            return GetMenuAttributeProperty(this.ContainerType, this.ItemAttributeType, propertyName, valueIfPropertyMissing);
        }

        protected static string GetMenuAttributeProperty(Type containerType, Type itemAttributeType, string propertyName)
        {
            return GetMenuAttributeProperty(containerType, itemAttributeType, propertyName, containerType.Name);
        }

        protected static string GetMenuAttributeProperty(Type containerType, Type itemAttributeType, string propertyName, string valueIfPropertyMissing)
        {
            Args.ThrowIfNull(containerType, nameof(containerType));
            Args.ThrowIfNull(itemAttributeType, nameof(itemAttributeType));
            Args.ThrowIfNullOrEmpty(propertyName, nameof(propertyName));
            Args.ThrowIfNullOrEmpty(valueIfPropertyMissing, nameof(valueIfPropertyMissing));

            object[] attributes = containerType.GetCustomAttributes(typeof(MenuAttribute), true);
            if (attributes.Length == 0)
            {
                return valueIfPropertyMissing;
            }
            if (attributes.Length == 1)
            {
                if (attributes[0] is MenuAttribute attribute && attribute.TryGetPropertyValue(propertyName, valueIfPropertyMissing, out string? value))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value!;
                    }
                    return valueIfPropertyMissing;
                }
            }

            foreach (object attribute in attributes)
            {
                if (attribute is MenuAttribute menuAttribute)
                {
                    if (menuAttribute.ItemAttributeType == itemAttributeType &&
                        menuAttribute.TryGetPropertyValue(propertyName, valueIfPropertyMissing, out string? val))
                    {
                        if (!string.IsNullOrEmpty(val))
                        {
                            return val!;
                        }
                        return valueIfPropertyMissing;
                    }
                }
            }

            return valueIfPropertyMissing;
        }

        /// <summary>
        /// Gets or sets the type that contains the menu item methods.
        /// </summary>
        public Type ContainerType { get; set; }

        /// <summary>
        /// Gets or sets the attribute type used to identify menu items.
        /// </summary>
        public Type ItemAttributeType { get; set; }

        /// <summary>
        /// Gets or sets the provider used to discover menu items.
        /// </summary>
        protected IMenuItemProvider MenuItemProvider
        {
            get;
            set;
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
        /// Gets the name of the menu, derived from the container type's <see cref="MenuAttribute"/>.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the display name shown to the user, derived from the container type's <see cref="MenuAttribute"/>.
        /// </summary>
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the description of the menu.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this menu is currently selected.
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the selector string used to navigate to this menu.
        /// </summary>
        public string Selector
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the header text displayed above the menu items.
        /// </summary>
        public string HeaderText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the footer text displayed below the menu items.
        /// </summary>
        public string FooterText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the console key used to exit the menu.
        /// </summary>
        public ConsoleKey ExitKey
        {
            get;
            private set;
        }

        IEnumerable<IMenuItem> _items = null!;

        /// <summary>
        /// Gets the collection of menu items, lazily initialized from the container type.
        /// </summary>
        public IEnumerable<IMenuItem> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = this.GetItems();
                }
                return _items;
            }
            protected set
            {
                _items = value;
            }
        }

        IMenuItem? _selectedItem;

        /// <summary>
        /// Gets or sets the currently selected menu item.
        /// </summary>
        public IMenuItem? SelectedItem
        {
            get
            {
                if(_selectedItem == null)
                {
                    _selectedItem = Items.FirstOrDefault(item => item.Selected);

                }
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
            }
        }

        /// <summary>
        /// Gets the zero-based index of the currently selected item, or -1 if no item is selected.
        /// </summary>
        public int SelectedItemIndex
        {
            get
            {
                if (SelectedItem == null)
                {
                    return -1;
                }
                int index = -1;
                foreach (IMenuItem item in Items)
                {
                    ++index;
                    if (item.Selected)
                    {
                        return index;
                    }
                }
                return -1;
            }
        }

        /// <summary>
        /// Gets a <see cref="MenuSpec"/> describing this menu's container and item attribute types.
        /// </summary>
        /// <returns>A new <see cref="MenuSpec"/> instance.</returns>
        public MenuSpec GetSpec()
        {
            return new MenuSpec(ContainerType, ItemAttributeType);
        }

        /// <summary>
        /// Selects a menu item based on the specified input.
        /// </summary>
        /// <param name="menuInput">The input identifying the item to select.</param>
        /// <returns>The selected menu item, or null if no match.</returns>
        public IMenuItem? SelectItem(IMenuInput menuInput)
        {
            return this.MenuItemSelector.SelectMenuItem(this, menuInput);
        }

        /// <summary>
        /// Selects a menu item by its selector string.
        /// </summary>
        /// <param name="itemSelector">The selector string of the item to select.</param>
        /// <returns>The selected menu item, or null if not found.</returns>
        public IMenuItem? SelectItem(string itemSelector)
        {
            IMenuItem? menuItem = GetItem(itemSelector);
            if (menuItem != null)
            {
                UnselectAll();
                menuItem.Selected = true;
                SelectedItem = menuItem;
                MenuItemSelected?.Invoke(this, new MenuEventArgs
                {
                    Menu = this,
                    MenuItem = menuItem,
                });
            }
            return menuItem;
        }

        /// <summary>
        /// Unselects all menu items.
        /// </summary>
        public void UnselectAll()
        {
            foreach(IMenuItem menuItem in Items)
            {
                menuItem.Selected = false;
            }
        }

        /// <summary>
        /// Gets a menu item by its selector string.
        /// </summary>
        /// <param name="selector">The selector string of the item.</param>
        /// <returns>The matching menu item, or null if not found.</returns>
        public IMenuItem? GetItem(string selector)
        {
            return Items.FirstOrDefault(item => item.Selector == selector);
        }

        /// <summary>
        /// Selects a menu item by its one-based item number.
        /// </summary>
        /// <param name="itemNumber">The one-based number of the item to select.</param>
        /// <returns>The selected menu item, or null if the number is out of range.</returns>
        public IMenuItem? SelectItemNumber(int itemNumber)
        {
            return SelectItem(itemNumber - 1);
        }

        /// <summary>
        /// Selects a menu item by its zero-based index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to select.</param>
        /// <returns>The selected menu item, or null if the index is out of range.</returns>
        public IMenuItem? SelectItem(int index)
        {
            IMenuItem[] itemArray = Items.ToArray();
            if (itemArray.Length > 0 && index >= 0 && index < itemArray.Length)
            {
                if(SelectedItem != null)
                {
                    SelectedItem.Selected = false;
                }
                IMenuItem menuItem = itemArray[index];
                menuItem.Selected = true;
                SelectedItem = menuItem;
                MenuItemSelected?.Invoke(this, new MenuEventArgs
                {
                    Menu = this,
                    MenuItem = menuItem,
                });
                return menuItem;
            }

            return null;
        }

        /// <summary>
        /// Selects the next menu item in the list.
        /// </summary>
        /// <returns>The newly selected menu item, or null if at the end of the list.</returns>
        public IMenuItem? SelectNextItem()
        {
            return SelectItem(SelectedItemIndex + 1);
        }

        /// <summary>
        /// Selects the previous menu item in the list.
        /// </summary>
        /// <returns>The newly selected menu item, or null if at the beginning of the list.</returns>
        public IMenuItem? SelectPreviousItem()
        {
            return SelectItem(SelectedItemIndex - 1);
        }

        /// <summary>
        /// Runs the specified menu item with optional input.
        /// </summary>
        /// <param name="menuItem">The menu item to run.</param>
        /// <param name="menuInput">Optional input to pass to the menu item.</param>
        /// <returns>The result of running the menu item.</returns>
        public IMenuItemRunResult RunItem(IMenuItem menuItem, IMenuInput? menuInput = null)
        {
            this.SelectedItem = menuItem;
            return this.RunItem(menuInput ?? MenuInput.Empty);
        }

        /// <summary>
        /// Runs the currently selected menu item with the specified input.
        /// </summary>
        /// <param name="menuInput">The input to pass to the menu item.</param>
        /// <returns>The result of running the menu item.</returns>
        public IMenuItemRunResult RunItem(IMenuInput menuInput)
        {
            IMenuItemRunResult runResult = new MenuItemRunResult()
            {
                Message = "No item selected"
            };

            if (SelectedItem != null)
            {
                MenuItemRunStarted?.Invoke(this, new MenuItemRunEventArgs
                {
                    Menu = this,
                    MenuItem = SelectedItem,
                    MenuInput = menuInput,
                });

                runResult = this.MenuItemRunner.RunMenuItem(SelectedItem, menuInput);

                MenuItemRunComplete?.Invoke(this, new MenuItemRunEventArgs
                {
                    Menu = this,
                    MenuItem = SelectedItem,
                    MenuInput = menuInput,
                    Result = runResult
                });
            }

            return runResult;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if(obj  == null) return false;
            if (obj is IMenu m)
            {
                if(m.ContainerType != null && m.ItemAttributeType != null)
                {
                    return m.ContainerType.Equals(this.ContainerType) && m.ItemAttributeType.Equals(this.ItemAttributeType);
                }
            }

            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ContainerType.GetHashCode() * 17 +
                this.ItemAttributeType.GetHashCode() * 17;
        }
    }
}
