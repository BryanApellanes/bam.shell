namespace Bam.Shell;

/// <summary>
/// Default implementation of <see cref="IMenuOptions"/> providing all required service dependencies for the menu system.
/// </summary>
public class MenuOptions : IMenuOptions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuOptions"/> class with all required dependencies.
    /// </summary>
    /// <param name="menuRenderer">The menu renderer.</param>
    /// <param name="menuHeaderRenderer">The menu header renderer.</param>
    /// <param name="menuFooterRenderer">The menu footer renderer.</param>
    /// <param name="menuProvider">The menu provider.</param>
    /// <param name="menuInputReader">The menu input reader.</param>
    /// <param name="menuInputCommandInterpreter">The menu input command interpreter.</param>
    /// <param name="menuItemRunner">The menu item runner.</param>
    /// <param name="menuItemRunResultRenderer">The renderer for menu item run results.</param>
    /// <param name="inputCommandResultRenderer">The renderer for input command results.</param>
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

    /// <inheritdoc/>
    public IMenuRenderer MenuRenderer { get; set; }

    /// <inheritdoc/>
    public IMenuHeaderRenderer MenuHeaderRenderer { get; set; }

    /// <inheritdoc/>
    public IMenuFooterRenderer MenuFooterRenderer { get; set; }

    /// <inheritdoc/>
    public IMenuProvider MenuProvider { get; set; }

    /// <inheritdoc/>
    public IMenuInputReader MenuInputReader { get; set; }

    /// <inheritdoc/>
    public IMenuInputCommandInterpreter MenuInputCommandInterpreter { get; set; }

    /// <inheritdoc/>
    public IMenuItemRunner MenuItemRunner { get; set; }

    /// <inheritdoc/>
    public IMenuItemRunResultRenderer MenuItemRunResultRenderer { get; set; }

    /// <inheritdoc/>
    public IInputCommandResultRenderer InputCommandResultRenderer { get; set; }
}