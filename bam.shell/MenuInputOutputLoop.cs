namespace Bam.Shell
{
    /// <summary>
    /// Implements the interactive input/output loop that drives the menu system, reading input and dispatching commands.
    /// </summary>
    internal class MenuInputOutputLoop : IMenuInputOutputLoop
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuInputOutputLoop"/> class.
        /// </summary>
        /// <param name="menuManager">The menu manager that owns this loop.</param>
        /// <param name="menuRenderer">The renderer used to display menus.</param>
        /// <param name="menuInputReader">The reader used to capture user input.</param>
        /// <param name="commandInterpreter">The interpreter for input commands.</param>
        /// <param name="menuItemRunResultRenderer">The renderer for menu item run results.</param>
        /// <param name="inputCommandResultRenderer">The renderer for input command results.</param>
        public MenuInputOutputLoop(IMenuManager menuManager, IMenuRenderer menuRenderer, IMenuInputReader menuInputReader, IMenuInputCommandInterpreter commandInterpreter, IMenuItemRunResultRenderer menuItemRunResultRenderer, IInputCommandResultRenderer inputCommandResultRenderer)
        {
            this.MenuManager = menuManager;
            this.MenuRenderer = menuRenderer;
            this.MenuInputReader = menuInputReader;
            this.MenuInputCommandInterpreter = commandInterpreter;
            this.MenuItemRunResultRenderer = menuItemRunResultRenderer;
            this.InputCommandResultRenderer = inputCommandResultRenderer;
        }

        /// <summary>
        /// Gets or sets the menu renderer.
        /// </summary>
        protected IMenuRenderer MenuRenderer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the menu input reader.
        /// </summary>
        protected IMenuInputReader MenuInputReader
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the renderer for menu item run results.
        /// </summary>
        protected IMenuItemRunResultRenderer MenuItemRunResultRenderer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the renderer for input command results.
        /// </summary>
        protected IInputCommandResultRenderer InputCommandResultRenderer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the menu input command interpreter.
        /// </summary>
        protected IMenuInputCommandInterpreter MenuInputCommandInterpreter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current menu input.
        /// </summary>
        protected IMenuInput MenuInput
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the menu manager that owns this input/output loop.
        /// </summary>
        public IMenuManager MenuManager
        {
            get;
            private set;
        }

        /// <summary>
        /// Occurs when the input/output loop is starting.
        /// </summary>
        public event EventHandler<MenuInputOutputLoopEventArgs> Starting;

        /// <summary>
        /// Occurs when reading input has completed.
        /// </summary>
        public event EventHandler<MenuInputOutputLoopEventArgs> ReadingInputComplete;

        /// <summary>
        /// Occurs when input is being processed.
        /// </summary>
        public event EventHandler<MenuInputOutputLoopEventArgs> ProcessingInput;

        /// <summary>
        /// Occurs when the input/output loop is ending.
        /// </summary>
        public event EventHandler<MenuInputOutputLoopEventArgs> Ending;

        /// <summary>
        /// Occurs when a menu item begins executing.
        /// </summary>
        public event EventHandler<MenuItemRunEventArgs> MenuItemRunStarted;

        /// <summary>
        /// Occurs when a menu item has finished executing.
        /// </summary>
        public event EventHandler<MenuItemRunEventArgs> MenuItemRunComplete;

        /// <summary>
        /// Ends the input/output loop using the current input reader.
        /// </summary>
        /// <returns>The menu manager.</returns>
        public IMenuManager End()
        {
            return End(this.MenuInputReader);
        }

        /// <summary>
        /// Ends the input/output loop, raising the <see cref="Ending"/> event.
        /// </summary>
        /// <param name="menuInputReader">The input reader to include in the event arguments.</param>
        /// <returns>The menu manager.</returns>
        public IMenuManager End(IMenuInputReader menuInputReader)
        {
            Ending?.Invoke(this, new MenuInputOutputLoopEventArgs
            {
                MenuInputOutputLoop = this,
                MenuInputReader = menuInputReader,
                MenuInput = this.MenuInput
            });

            return MenuManager;
        }

        /// <summary>
        /// Starts the input/output loop using the current input reader.
        /// </summary>
        public void Start()
        {
            Start(this.MenuInputReader);
        }

        /// <summary>
        /// Starts the input/output loop using the specified input reader, raising the <see cref="Starting"/> event.
        /// </summary>
        /// <param name="menuInputReader">The input reader to use for the loop.</param>
        public void Start(IMenuInputReader menuInputReader)
        {
            Starting?.Invoke(this, new MenuInputOutputLoopEventArgs()
            {
                MenuInputOutputLoop = this
            });
            
            Loop(menuInputReader);
        }

        protected void Loop(IMenuInputReader menuInputReader)
        {
            this.MenuInputReader = menuInputReader;
            this.MenuManager.RenderMenu();
            this.MenuInput = menuInputReader.ReadMenuInput();
            
            this.MenuManager.AddMenuItemRunStartedHandler((sender, args) => MenuItemRunStarted?.Invoke(this, args));
            this.MenuManager.AddMenuItemRunCompleteHandler((sender, args) => MenuItemRunComplete?.Invoke(this, args));

            Loop(menuInputReader, this.MenuInput);
        }

        protected void Loop(IMenuInputReader menuInputReader, IMenuInput menuInput)
        {
            while (true)
            {
                this.MenuInput = menuInput;
                this.MenuManager.RerenderMenu(menuInput);
                if (menuInput.Enter)
                {
                    if (this.MenuInputCommandInterpreter.InterpretInput(this.MenuManager, menuInput, out IInputCommandResults interpreterResult))
                    {
                        foreach(IInputCommandResult? commandResult in interpreterResult.Results)
                        {
                            if (commandResult != null)
                            {
                                menuInput.Input.Clear();
                                InputCommandResultRenderer.RenderInputCommandResult(commandResult);
                                this.MenuRenderer.RenderDivider();
                            }
                        }
                    }
                    else
                    {
                        IMenuItemRunResult? menuItemRunResult = this.MenuManager.RunMenuItem(menuInput);
                        if (menuItemRunResult != null)
                        {
                            menuInput.Input.Clear();
                            MenuItemRunResultRenderer.RenderMenuItemRunResult(menuItemRunResult);
                            this.MenuRenderer.RenderDivider();
                        }
                    }
                }
                if(menuInput.Exit)
                {
                    End(menuInputReader);
                    return;
                }
                menuInput = ReadInput(menuInputReader);
            }
        }
        protected IMenuInput ReadInput(IMenuInputReader inputReader)
        {
            MenuInputOutputLoopEventArgs eventArgs = new MenuInputOutputLoopEventArgs
            {
                MenuInputOutputLoop = this,
                MenuInputReader = inputReader,
                MenuInput = inputReader.ReadMenuInput(),
            };

            ReadingInputComplete?.Invoke(this, eventArgs);
            return eventArgs.MenuInput;
        }
    }
}
