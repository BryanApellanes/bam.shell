using Bam.Logging;
using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Manages menus, menu selection, rendering, and the input/output loop for a shell-based menu system.
    /// </summary>
    public class MenuManager : IMenuManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuManager"/> class.
        /// </summary>
        /// <param name="menuRenderer">The renderer used to display menus.</param>
        /// <param name="menuProvider">The provider used to create and retrieve menus.</param>
        /// <param name="menuInputReader">The reader used to capture user input.</param>
        /// <param name="menuInputCommandInterpreter">The interpreter used to process input commands.</param>
        /// <param name="menuItemRunResultRenderer">The renderer used to display menu item run results.</param>
        /// <param name="inputCommandResultRenderer">The renderer used to display input command results.</param>
        public MenuManager(IMenuRenderer menuRenderer, IMenuProvider menuProvider, IMenuInputReader menuInputReader,  IMenuInputCommandInterpreter menuInputCommandInterpreter, IMenuItemRunResultRenderer menuItemRunResultRenderer, IInputCommandResultRenderer inputCommandResultRenderer)
        {
            this.MenusBySelector = new Dictionary<string, IMenu>();
            this.Menus = new List<IMenu>();
            this.MenuRenderer = menuRenderer;
            this.MenuProvider = menuProvider;
            this.MenuInputReader = menuInputReader;
            this.MenuInputCommandInterpreter = menuInputCommandInterpreter;
            this.MenuItemRunResultRenderer = menuItemRunResultRenderer;
            this.InputCommandResultRenderer = inputCommandResultRenderer;

            this.StateUpdating += OnStateUpdating;
            this.DuplicateMenuSelectorSpecified += OnDuplicateMenuSelectorSpecified;
        }

        private void OnDuplicateMenuSelectorSpecified(object? sender, DuplicateMenuSelectorEventArgs e)
        {
            Log.Warn("Duplicate menu selectors specified: [:{0}] ({1}) and [:{2}] ({3})", e.FirstMenu.Selector, e.FirstMenu.Name, e.SecondMenu.Selector, e.SecondMenu.Name);
        }
        
        /// <summary>
        /// Creates a new <see cref="MenuManager"/> from the specified menu options.
        /// </summary>
        /// <param name="options">The menu options containing all required dependencies.</param>
        /// <returns>A new <see cref="MenuManager"/> instance configured with the specified options.</returns>
        public static MenuManager FromOptions(IMenuOptions options)
        {
            return new MenuManager(options.MenuRenderer,

                options.MenuProvider,
                options.MenuInputReader,
                options.MenuInputCommandInterpreter,
                options.MenuItemRunResultRenderer,
                options.InputCommandResultRenderer);
        }
        
        /// <summary>
        /// Occurs when a menu item is selected.
        /// </summary>
        public event EventHandler<MenuEventArgs> MenuItemSelected;

        /// <summary>
        /// Occurs before the menu manager state is updated in response to input.
        /// </summary>
        public event EventHandler<MenuManagerUpdateStateEventArgs> StateUpdating;

        /// <summary>
        /// Occurs after the menu manager state has been updated in response to input.
        /// </summary>
        public event EventHandler<MenuManagerUpdateStateEventArgs> StateUpdated;

        /// <summary>
        /// Occurs when two menus are registered with the same selector.
        /// </summary>
        public event EventHandler<DuplicateMenuSelectorEventArgs> DuplicateMenuSelectorSpecified;
        /// <summary>
        /// Gets or sets the menu renderer.
        /// </summary>
        protected IMenuRenderer MenuRenderer { get; set; }

        /// <summary>
        /// Gets or sets the menu provider.
        /// </summary>
        protected IMenuProvider MenuProvider { get; set; }

        /// <summary>
        /// Gets or sets the menu input reader.
        /// </summary>
        protected IMenuInputReader MenuInputReader { get; set; }

        /// <summary>
        /// Gets or sets the menu input command interpreter.
        /// </summary>
        protected IMenuInputCommandInterpreter MenuInputCommandInterpreter { get; set; }

        /// <summary>
        /// Gets or sets the renderer for menu item run results.
        /// </summary>
        protected IMenuItemRunResultRenderer MenuItemRunResultRenderer { get; set; }

        /// <summary>
        /// Gets or sets the renderer for input command results.
        /// </summary>
        protected IInputCommandResultRenderer InputCommandResultRenderer { get; set; }

        private bool _settingCurrentMenu;

        private void SetMenuIndex()
        {
            if (_settingCurrentMenu)
            {
                return;
            }

            if (_currentMenu != null)
            {
                for (int i = 0; i < Menus.Count; i++)
                {
                    if (Menus[i] == _currentMenu)
                    {
                        CurrentMenuIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a menu to the manager, indexing it by its selector.
        /// </summary>
        /// <param name="menu">The menu to add.</param>
        public void AddMenu(IMenu menu)
        {
            if (menu == null)
            {
                return;
            }
            if (!MenusBySelector.ContainsValue(menu))
            {
                if (MenusBySelector.ContainsKey(menu.Selector))
                {
                    DuplicateMenuSelectorSpecified?.Invoke(this, new DuplicateMenuSelectorEventArgs
                    {
                        FirstMenu = MenusBySelector[menu.Selector],
                        SecondMenu = menu
                    });
                    MenusBySelector[menu.Selector] = menu;
                }
                else
                {
                    MenusBySelector.Add(menu.Selector, menu);
                }
            }

            if (!Menus.Contains(menu))
            {
                Menus.Add(menu);
            }
        }

        static ConsoleKey _exitKey = ConsoleKey.Escape;
        /// <summary>
        /// Gets or sets the console key used to exit the menu system. Defaults to <see cref="ConsoleKey.Escape"/>.
        /// </summary>
        public static ConsoleKey ExitKey
        {
            get => _exitKey;
            set => _exitKey = value;
        }

        IMenu? _currentMenu;

        /// <summary>
        /// Gets the currently selected menu, lazily initializing from the provider if necessary.
        /// </summary>
        public IMenu? CurrentMenu
        {
            get
            {
                if(_currentMenu == null)
                {
                    _settingCurrentMenu = true;
                    try
                    {
                        if (Menus.Any())
                        {
                            _currentMenu = Menus.FirstOrDefault();
                        }
                        else
                        {
                            _currentMenu = MenuProvider.GetDefaultMenu();
                            if(_currentMenu != null)
                            {
                                AddMenu(_currentMenu);
                            }
                        }
                    }
                    finally
                    {
                        _settingCurrentMenu = false;
                    }
                    SetMenuIndex();
                }
                return _currentMenu;
            }
            private set
            {
                _currentMenu = value;
                SetMenuIndex();
            }
        }

        /// <summary>
        /// Gets the index of the currently selected menu within the <see cref="Menus"/> list.
        /// </summary>
        public int CurrentMenuIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the dictionary of menus indexed by their selector strings.
        /// </summary>
        public Dictionary<string, IMenu> MenusBySelector { get; private set; }

        /// <summary>
        /// Gets the list of all registered menus.
        /// </summary>
        public IList<IMenu> Menus { get; private set; }

        /// <summary>
        /// Subscribes the specified handler to the <see cref="IMenu.MenuItemRunStarted"/> event on all registered menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        public void AddMenuItemRunStartedHandler(EventHandler<MenuItemRunEventArgs> handler)
        {
            foreach (IMenu menu in Menus)
            {
                menu.SubscribeOnce(nameof(menu.MenuItemRunStarted), handler);
            }
        }

        /// <summary>
        /// Subscribes the specified handler to the <see cref="IMenu.MenuItemRunComplete"/> event on all registered menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        public void AddMenuItemRunCompleteHandler(EventHandler<MenuItemRunEventArgs> handler)
        {
            foreach (IMenu menu in Menus)
            {
                menu.SubscribeOnce(nameof(menu.MenuItemRunComplete), handler);
            }
        }

        /// <summary>
        /// Subscribes the specified handler to the <see cref="IMenu.MenuItemSelected"/> event on all registered menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        public void AddMenuItemSelectedHandler(EventHandler<MenuEventArgs> handler)
        {
            foreach(IMenu menu in Menus)
            {
                menu.MenuItemSelected += handler;
            }
        }

        /// <summary>
        /// Subscribes the specified handler to the <see cref="IMenu.MenuItemSelectionChanged"/> event on all registered menus.
        /// </summary>
        /// <param name="handler">The event handler to subscribe.</param>
        public void AddMenuItemSelectionChangedHandler(EventHandler<MenuEventArgs> handler)
        {
            foreach(IMenu menu in Menus)
            {
                menu.MenuItemSelectionChanged += handler;
            }
        }

        /// <summary>
        /// Load any menus defined in MenuSpecs.LoadList and any menus found in the entry assembly.
        /// </summary>
        public void LoadMenus()
        {
            if (MenuSpecs.LoadList.Any())
            {
                LoadMenus(MenuSpecs.LoadList);
            }
            Assembly? entryAssembly = Assembly.GetEntryAssembly();
            if(entryAssembly != null)
            {
                LoadMenus(entryAssembly);
            }
        }

        /// <summary>
        /// Loads menus by scanning the specified assembly for types decorated with <see cref="MenuAttribute"/>.
        /// </summary>
        /// <param name="assembly">The assembly to scan for menu types.</param>
        public void LoadMenus(Assembly assembly)
        {
            LoadMenus(MenuSpecs.Scan(assembly));
        }

        /// <summary>
        /// Loads menus from the specified collection of menu specifications.
        /// </summary>
        /// <param name="menuSpecs">The menu specifications to create menus from.</param>
        public void LoadMenus(IEnumerable<MenuSpecs> menuSpecs)
        {
            foreach(MenuSpecs menuSpec in menuSpecs)
            {
                foreach(IMenu menu in menuSpec.CreateMenus(this.MenuProvider))
                {
                    this.AddMenu(menu);
                }
            }
        }

        /// <summary>
        /// Gets or creates a menu for the specified container type, adding it to the manager if not already present.
        /// </summary>
        /// <param name="type">The container type to get the menu for.</param>
        /// <returns>The menu for the specified type, or null if none could be created.</returns>
        public IMenu? GetMenu(Type type)
        {
            IMenu menu = this.MenuProvider.GetMenu(type);
            if (menu != null)
            {
                if (!Menus.Contains(menu))
                {
                    AddMenu(menu);
                }
            }

            return menu;
        }

        /// <summary>
        /// Gets a menu by its selector string, adding it to the manager if not already present.
        /// </summary>
        /// <param name="selector">The selector string identifying the menu.</param>
        /// <returns>The menu matching the selector, or null if not found.</returns>
        public IMenu? GetMenu(string selector)
        {
            IMenu? menu = this.MenuProvider.GetMenu(selector);
            if (menu != null)
            {
                if (!Menus.Contains(menu))
                {
                    AddMenu(menu);
                }
            }

            return menu;
        }

        /// <summary>
        /// Creates a menu for the specified type and adds it to the manager.
        /// </summary>
        /// <param name="type">The container type to create a menu for.</param>
        /// <returns>The created menu, or null if the type does not define a menu.</returns>
        public IMenu? AddMenu(Type type)
        {
            IMenu? menu = GetMenu(type);
            if (menu != null)
            {
                if(!Menus.Contains(menu))
                {
                    AddMenu(menu);
                }
            }

            return menu;
        }

        /// <summary>
        /// Gets the currently selected menu item from the current menu.
        /// </summary>
        /// <returns>The selected menu item, or null if no menu or item is selected.</returns>
        public IMenuItem? GetSelectedMenuItem()
        {
            if (CurrentMenu != null && CurrentMenu.SelectedItem != null)
            {
                return CurrentMenu.SelectedItem;
            }
            return null;
        }

        /// <summary>
        /// Selects the menu matching the specified selector, making it the current menu.
        /// </summary>
        /// <param name="selector">The selector string identifying the menu to select.</param>
        /// <returns>The selected menu, or null if no menu matches the selector.</returns>
        public IMenu? SelectMenu(string selector)
        {
            IMenu? menu = GetMenu(selector);
            if (menu != null)
            {
                menu.Selected = true;
                foreach (IMenu existingMenu in Menus)
                {
                    existingMenu.Selected = false;
                }
                this.CurrentMenu = menu;
            }

            return menu;
        }

        /// <summary>
        /// Selects the next menu in the list, if one exists.
        /// </summary>
        /// <returns>The newly selected current menu.</returns>
        public IMenu? SelectNextMenu()
        {
            int next = this.CurrentMenuIndex + 1;
            if (next < Menus.Count)
            {
                CurrentMenu = Menus[this.CurrentMenuIndex + 1];
            }
            return CurrentMenu;
        }

        /// <summary>
        /// Selects the previous menu in the list, if one exists.
        /// </summary>
        /// <returns>The newly selected current menu.</returns>
        public IMenu? SelectPreviousMenu()
        {
            if (CurrentMenuIndex > 0)
            {
                CurrentMenu = Menus[CurrentMenuIndex - 1];
            }
            return CurrentMenu;
        }

        /// <summary>
        /// Sets the specified menu as the current menu.
        /// </summary>
        /// <param name="menu">The menu to select.</param>
        /// <returns>The selected menu.</returns>
        public IMenu? SelectMenu(IMenu menu)
        {
            CurrentMenu = menu;
            return menu;
        }

        /// <summary>
        /// Selects the next or previous menu based on the navigation direction specified in the menu input.
        /// </summary>
        /// <param name="menuInput">The input containing navigation direction.</param>
        /// <returns>The newly selected current menu.</returns>
        public IMenu? SelectMenu(IMenuInput menuInput)
        {
            if (menuInput.NextMenu)
            {
                return SelectNextMenu();
            }
            else if (menuInput.PreviousMenu)
            {
                return SelectPreviousMenu();
            }

            return CurrentMenu;
        }

        /// <summary>
        /// Selects a menu item within the current menu based on user input.
        /// </summary>
        /// <param name="menuInput">The input identifying the menu item to select.</param>
        /// <returns>The selected menu item, or null if no item matches.</returns>
        public IMenuItem? SelectMenuItem(IMenuInput menuInput)
        {
            return CurrentMenu?.SelectItem(menuInput);
        }

        /// <summary>
        /// Selects a menu item within the specified menu based on user input.
        /// </summary>
        /// <param name="menu">The menu to select an item from.</param>
        /// <param name="menuInput">The input identifying the menu item to select.</param>
        /// <returns>The selected menu item, or null if no item matches.</returns>
        public IMenuItem? SelectMenuItem(IMenu menu, IMenuInput menuInput)
        {
            return menu.SelectItem(menuInput);
        }

        /// <summary>
        /// Renders the current menu. Throws if no menu is currently selected.
        /// </summary>
        public void RenderMenu()
        {
            if (CurrentMenu == null)
            {
                throw new ArgumentNullException(nameof(CurrentMenu));
            }

            RenderMenu(CurrentMenu);
        }

        /// <summary>
        /// Updates the menu manager state by raising the <see cref="StateUpdating"/> and <see cref="StateUpdated"/> events.
        /// </summary>
        /// <param name="menu">The current menu.</param>
        /// <param name="menuInput">The input that triggered the state update.</param>
        protected virtual void UpdateState(IMenu? menu, IMenuInput menuInput)
        {
            Args.ThrowIfNull(menu, nameof(menu));

            StateUpdating?.Invoke(this, new MenuManagerUpdateStateEventArgs
            {
                Menu = menu,
                MenuInput = menuInput                
            });            

            StateUpdated?.Invoke(this, new MenuManagerUpdateStateEventArgs
            {
                Menu = menu,
                MenuInput = menuInput
            });
        }

        /// <summary>
        /// Handles the <see cref="StateUpdating"/> event by selecting menu items or menus based on the input.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments containing the menu and input.</param>
        protected virtual void OnStateUpdating(object? sender, MenuManagerUpdateStateEventArgs e)
        {
            if (e.MenuInput.IsMenuItemNavigation || e.MenuInput.ItemNumber > 0)
            {
                SelectMenuItem(e.MenuInput);
            }
            else if (e.MenuInput.IsMenuNavigation)
            {
                SelectMenu(e.MenuInput);
            }
            else if (e.MenuInput.IsSelector)
            {
                if (IsMenuSelector(e.MenuInput, out IMenu? menu))
                {
                    if (menu != null)
                    {
                        SelectMenu(menu);
                        e.MenuInput.Input.Clear();
                    }
                }
                else
                {
                    SelectMenuItem(e.MenuInput);
                }
            }
        }

        /// <summary>
        /// Determines whether the input matches a menu item selector within the specified menu.
        /// </summary>
        /// <param name="menu">The menu to search for a matching item.</param>
        /// <param name="menuInput">The input containing the selector.</param>
        /// <param name="menuItem">When this method returns, contains the matching menu item, or null.</param>
        /// <returns>True if a matching menu item was found; otherwise, false.</returns>
        protected bool IsMenuItemSelector(IMenu menu, IMenuInput menuInput, out IMenuItem? menuItem)
        {
            menuItem = menu.SelectItem(menuInput.Selector);
            return menuItem != null;
        }

        /// <summary>
        /// Determines whether the input matches a menu selector.
        /// </summary>
        /// <param name="menuInput">The input containing the selector.</param>
        /// <param name="menu">When this method returns, contains the matching menu, or null.</param>
        /// <returns>True if a matching menu was found; otherwise, false.</returns>
        public bool IsMenuSelector(IMenuInput menuInput, out IMenu? menu)
        {
            menu = GetMenu(menuInput.Selector);
            return menu != null;
        }

        /// <summary>
        /// Re-renders the current menu after updating state based on the specified input.
        /// </summary>
        /// <param name="menuInput">The input that triggered the re-render.</param>
        public void RerenderMenu(IMenuInput menuInput)
        {
            this.UpdateState(CurrentMenu, menuInput);
            if (CurrentMenu != null && menuInput != null)
            {                
                this.RerenderMenu(CurrentMenu, menuInput);
            }
        }

        /// <summary>
        /// Re-renders the specified menu using the menu renderer.
        /// </summary>
        /// <param name="menu">The menu to re-render.</param>
        /// <param name="menuInput">The input that triggered the re-render.</param>
        protected void RerenderMenu(IMenu menu, IMenuInput menuInput)
        {
            this.MenuRenderer.RerenderMenu(menu, menuInput, Menus.ToArray());
        }

        /// <summary>
        /// Renders the specified menu.
        /// </summary>
        /// <param name="menu">The menu to render.</param>
        public void RenderMenu(IMenu menu)
        {
            this.MenuRenderer.RenderMenu(menu, Menus.ToArray());
        }

        /// <summary>
        /// Runs the specified menu item within the current menu.
        /// </summary>
        /// <param name="menuItem">The menu item to run.</param>
        /// <param name="menuInput">Optional input to pass to the menu item.</param>
        /// <returns>The result of running the menu item, or a result with an error message if no menu is selected.</returns>
        public IMenuItemRunResult? RunMenuItem(IMenuItem menuItem, IMenuInput? menuInput = null)
        {
            if (CurrentMenu != null)
            {
                return this.CurrentMenu.RunItem(menuItem, menuInput);
            }

            return new MenuItemRunResult { Message = "No menu selected" };
        }

        /// <summary>
        /// Runs the currently selected menu item using the specified input.
        /// </summary>
        /// <param name="menuInput">The input to pass to the menu item.</param>
        /// <returns>The result of running the menu item, or a result with an error message if no menu is selected.</returns>
        public IMenuItemRunResult? RunMenuItem(IMenuInput menuInput)
        {
            if (CurrentMenu != null)
            {
                return this.CurrentMenu.RunItem(menuInput);
            }

            return new MenuItemRunResult { Message = "No menu selected" };
        }

        /// <summary>
        /// Loads all menus and starts the interactive input/output loop that processes user input until exit.
        /// </summary>
        /// <returns>This menu manager instance.</returns>
        public IMenuManager StartInputOutputLoop()
        {
            this.LoadMenus();
            MenuInputOutputLoop loop = new MenuInputOutputLoop(this, this.MenuRenderer, this.MenuInputReader, this.MenuInputCommandInterpreter, this.MenuItemRunResultRenderer, this.InputCommandResultRenderer);
            loop.Ending += (sender, args) => Environment.Exit(args.MenuInput.ExitCode);
            loop.Start();

            return this;
        }
    }
}
