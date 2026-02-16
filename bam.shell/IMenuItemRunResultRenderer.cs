namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for rendering menu item run results to the output.
    /// </summary>
    public interface IMenuItemRunResultRenderer
    {
        /// <summary>
        /// Renders the specified menu item run result.
        /// </summary>
        /// <param name="menuItemRunResult">The menu item run result to render.</param>
        void RenderMenuItemRunResult(IMenuItemRunResult menuItemRunResult);
    }
}
