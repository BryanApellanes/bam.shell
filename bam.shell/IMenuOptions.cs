namespace Bam.Shell;

public interface IMenuOptions
{
    IMenuRenderer MenuRenderer { get; set; } 
    IMenuHeaderRenderer MenuHeaderRenderer { get; set; } 
    IMenuFooterRenderer MenuFooterRenderer { get; set; }
    IMenuProvider MenuProvider { get; set; }
    IMenuInputReader MenuInputReader { get; set; }
    IMenuInputCommandInterpreter MenuInputCommandInterpreter { get; set; }
    IMenuItemRunner MenuItemRunner { get; set; }
    IMenuItemRunResultRenderer MenuItemRunResultRenderer { get; set; } 
    IInputCommandResultRenderer InputCommandResultRenderer { get; set; }
}