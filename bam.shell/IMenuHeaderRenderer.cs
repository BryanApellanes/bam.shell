namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for rendering a menu header.
    /// </summary>
    public interface IMenuHeaderRenderer
    {
        /// <summary>
        /// Renders the header for the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose header to render.</param>
        void RenderMenuHeader(IMenu menu);
    }
}
