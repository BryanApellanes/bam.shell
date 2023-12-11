using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public abstract class MenuRenderer : IMenuRenderer
    {
        public MenuRenderer(IMenuHeaderRenderer headerRenderer, IMenuFooterRenderer footerRenderer, IMenuInputReader inputReader, IMenuInputCommandRenderer inputCommandRenderer)
        {
            this.HeaderRenderer = headerRenderer;
            this.FooterRenderer = footerRenderer;
            this.InputReader = inputReader;
            this.InputCommandRenderer = inputCommandRenderer;
        }

        public string? Divider
        {
            get;
            protected set;
        }

        protected IMenuHeaderRenderer HeaderRenderer { get; private set; }
        protected IMenuFooterRenderer FooterRenderer { get; private set; }

        protected IMenuInputCommandRenderer InputCommandRenderer { get; private set; }

        protected IMenuInputReader InputReader
        {
            get;
            private set;
        }

        protected abstract void RenderItems(IMenu menu);

        public abstract void RerenderMenu(IMenu menu, IMenuInput menuInput, params IMenu[] otherMenus);

        public abstract void RenderMenu(IMenu menu, params IMenu[] otherMenus);

        public abstract void RenderDivider();

        public abstract void RenderInputCommands(IMenu menu);
    }
}
