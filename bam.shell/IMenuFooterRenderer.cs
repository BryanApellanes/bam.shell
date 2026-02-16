namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for rendering a menu footer.
    /// </summary>
    public interface IMenuFooterRenderer
    {
        /// <summary>
        /// Renders the footer for the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose footer to render.</param>
        /// <param name="otherMenus">Other menus available for navigation.</param>
        void RenderMenuFooter(IMenu menu, params IMenu[] otherMenus);
    }
}
