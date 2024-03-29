﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    internal class MenuInputOutputLoop : IMenuInputOutputLoop
    {
        public MenuInputOutputLoop(IMenuManager menuManager, IMenuRenderer menuRenderer, IMenuInputReader menuInputReader, IMenuInputCommandInterpreter commandInterpreter, IMenuItemRunResultRenderer menuItemRunResultRenderer, IInputCommandResultRenderer inputCommandResultRenderer)
        {
            this.MenuManager = menuManager;
            this.MenuRenderer = menuRenderer;
            this.MenuInputReader = menuInputReader;
            this.MenuInputCommandInterpreter = commandInterpreter;
            this.MenuItemRunResultRenderer = menuItemRunResultRenderer;
            this.InputCommandResultRenderer = inputCommandResultRenderer;
        }

        protected IMenuRenderer MenuRenderer
        { 
            get; 
            set; 
        }

        protected IMenuInputReader MenuInputReader
        {
            get;
            set;
        }

        protected IMenuItemRunResultRenderer MenuItemRunResultRenderer 
        {
            get; 
            set; 
        }

        protected IInputCommandResultRenderer InputCommandResultRenderer
        {
            get;
            set;
        }

        protected IMenuInputCommandInterpreter MenuInputCommandInterpreter 
        {
            get; 
            set; 
        }

        protected IMenuInput MenuInput 
        {
            get; 
            set; 
        }

        public IMenuManager MenuManager
        {
            get;
            private set;
        }

        public event EventHandler<MenuInputOutputLoopEventArgs> Starting;
        public event EventHandler<MenuInputOutputLoopEventArgs> ReadingInputComplete;
        public event EventHandler<MenuInputOutputLoopEventArgs> ProcessingInput;
        public event EventHandler<MenuInputOutputLoopEventArgs> Ending;
        
        public event EventHandler<MenuItemRunEventArgs> MenuItemRunStarted;
        public event EventHandler<MenuItemRunEventArgs> MenuItemRunComplete;

        public IMenuManager End()
        {
            return End(this.MenuInputReader);
        }

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

        public void Start()
        {
            Start(this.MenuInputReader);
        }

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
            }
            else
            {
                Loop(menuInputReader, ReadInput(menuInputReader));
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
