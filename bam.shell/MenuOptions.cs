namespace Bam.Shell;

public class MenuOptions : IMenuOptions
{
    public MenuOptions(IMenuRenderer menuRenderer, IMenuHeaderRenderer menuHeaderRenderer,
        IMenuFooterRenderer menuFooterRenderer, IMenuProvider menuProvider, IMenuInputReader menuInputReader,
        IMenuInputCommandInterpreter menuInputCommandInterpreter, IMenuItemRunner menuItemRunner,
        IMenuItemRunResultRenderer menuItemRunResultRenderer, IInputCommandResultRenderer inputCommandResultRenderer)
    {
        this.MenuRenderer = menuRenderer;
        this.MenuHeaderRenderer = menuHeaderRenderer;
        this.MenuFooterRenderer = menuFooterRenderer;
        this.MenuProvider = menuProvider;
        this.MenuInputReader = menuInputReader;
        this.MenuInputCommandInterpreter = menuInputCommandInterpreter;
        this.MenuItemRunner = menuItemRunner;
        this.MenuItemRunResultRenderer = menuItemRunResultRenderer;
        this.InputCommandResultRenderer = inputCommandResultRenderer;
    }

    public IMenuRenderer MenuRenderer { get; set; }
    public IMenuHeaderRenderer MenuHeaderRenderer { get; set; }
    public IMenuFooterRenderer MenuFooterRenderer { get; set; }
    public IMenuProvider MenuProvider { get; set; }
    public IMenuInputReader MenuInputReader { get; set; }
    public IMenuInputCommandInterpreter MenuInputCommandInterpreter { get; set; }
    public IMenuItemRunner MenuItemRunner { get; set; }
    public IMenuItemRunResultRenderer MenuItemRunResultRenderer { get; set; }
    public IInputCommandResultRenderer InputCommandResultRenderer { get; set; }
}