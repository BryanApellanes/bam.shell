namespace Bam.Shell
{
    /// <summary>
    /// Abstract base class for rendering menus to the console.
    /// </summary>
    public abstract class MenuRenderer : IMenuRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuRenderer"/> class.
        /// </summary>
        /// <param name="headerRenderer">The renderer for menu headers.</param>
        /// <param name="footerRenderer">The renderer for menu footers.</param>
        /// <param name="inputReader">The reader for menu input.</param>
        /// <param name="inputCommandRenderer">The renderer for input commands.</param>
        public MenuRenderer(IMenuHeaderRenderer headerRenderer, IMenuFooterRenderer footerRenderer, IMenuInputReader inputReader, IMenuInputCommandRenderer inputCommandRenderer)
        {
            this.HeaderRenderer = headerRenderer;
            this.FooterRenderer = footerRenderer;
            this.InputReader = inputReader;
            this.InputCommandRenderer = inputCommandRenderer;
        }

        /// <summary>
        /// Gets the divider string used to separate menu sections.
        /// </summary>
        public string? Divider
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the header renderer.
        /// </summary>
        protected IMenuHeaderRenderer HeaderRenderer { get; private set; }

        /// <summary>
        /// Gets the footer renderer.
        /// </summary>
        protected IMenuFooterRenderer FooterRenderer { get; private set; }

        /// <summary>
        /// Gets the input command renderer.
        /// </summary>
        protected IMenuInputCommandRenderer InputCommandRenderer { get; private set; }

        /// <summary>
        /// Gets the input reader.
        /// </summary>
        protected IMenuInputReader InputReader
        {
            get;
            private set;
        }

        /// <summary>
        /// Renders the items of the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose items to render.</param>
        protected abstract void RenderItems(IMenu menu);

        /// <summary>
        /// Re-renders the specified menu with the current input state.
        /// </summary>
        /// <param name="menu">The menu to re-render.</param>
        /// <param name="menuInput">The current menu input.</param>
        /// <param name="otherMenus">Other menus available for navigation.</param>
        public abstract void RerenderMenu(IMenu menu, IMenuInput menuInput, params IMenu[] otherMenus);

        /// <summary>
        /// Renders the specified menu for the first time.
        /// </summary>
        /// <param name="menu">The menu to render.</param>
        /// <param name="otherMenus">Other menus available for navigation.</param>
        public abstract void RenderMenu(IMenu menu, params IMenu[] otherMenus);

        /// <summary>
        /// Renders a visual divider to the output.
        /// </summary>
        public abstract void RenderDivider();

        /// <summary>
        /// Renders the available input commands for the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose input commands to render.</param>
        public abstract void RenderInputCommands(IMenu menu);
    }
}
