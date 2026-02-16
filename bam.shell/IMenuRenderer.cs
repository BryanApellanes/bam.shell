namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for rendering menus to the output.
    /// </summary>
    public interface IMenuRenderer
    {
        /// <summary>
        /// Re-renders the specified menu with the current input state.
        /// </summary>
        /// <param name="menu">The menu to re-render.</param>
        /// <param name="menuInput">The current menu input.</param>
        /// <param name="otherMenus">Other menus available for navigation.</param>
        void RerenderMenu(IMenu menu, IMenuInput menuInput, params IMenu[] otherMenus);

        /// <summary>
        /// Renders the specified menu for the first time.
        /// </summary>
        /// <param name="menu">The menu to render.</param>
        /// <param name="otherMenus">Other menus available for navigation.</param>
        void RenderMenu(IMenu menu, params IMenu[] otherMenus);

        /// <summary>
        /// Renders a visual divider to the output.
        /// </summary>
        void RenderDivider();

        /// <summary>
        /// Renders the available input commands for the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose input commands to render.</param>
        void RenderInputCommands(IMenu menu);
    }
}
