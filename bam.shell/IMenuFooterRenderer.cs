namespace Bam.Shell
{
    public interface IMenuFooterRenderer
    {
        void RenderMenuFooter(IMenu menu, params IMenu[] otherMenus);
    }
}
