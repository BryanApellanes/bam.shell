namespace Bam.Shell;

/// <summary>
/// Defines the configuration options for the menu system, providing all required service dependencies.
/// </summary>
public interface IMenuOptions
{
    /// <summary>
    /// Gets or sets the menu renderer.
    /// </summary>
    IMenuRenderer MenuRenderer { get; set; }

    /// <summary>
    /// Gets or sets the menu header renderer.
    /// </summary>
    IMenuHeaderRenderer MenuHeaderRenderer { get; set; }

    /// <summary>
    /// Gets or sets the menu footer renderer.
    /// </summary>
    IMenuFooterRenderer MenuFooterRenderer { get; set; }

    /// <summary>
    /// Gets or sets the menu provider.
    /// </summary>
    IMenuProvider MenuProvider { get; set; }

    /// <summary>
    /// Gets or sets the menu input reader.
    /// </summary>
    IMenuInputReader MenuInputReader { get; set; }

    /// <summary>
    /// Gets or sets the menu input command interpreter.
    /// </summary>
    IMenuInputCommandInterpreter MenuInputCommandInterpreter { get; set; }

    /// <summary>
    /// Gets or sets the menu item runner.
    /// </summary>
    IMenuItemRunner MenuItemRunner { get; set; }

    /// <summary>
    /// Gets or sets the renderer for menu item run results.
    /// </summary>
    IMenuItemRunResultRenderer MenuItemRunResultRenderer { get; set; }

    /// <summary>
    /// Gets or sets the renderer for input command results.
    /// </summary>
    IInputCommandResultRenderer InputCommandResultRenderer { get; set; }
}