namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for rendering available input commands for a menu.
    /// </summary>
    public interface IMenuInputCommandRenderer
    {
        /// <summary>
        /// Renders the available input commands for the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose input commands to render.</param>
        void RenderMenuInputCommands(IMenu menu);
    }
}
