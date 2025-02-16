namespace Bam.Shell
{
    public interface IMenuItemProvider<TAttr> : IMenuItemProvider where TAttr : Attribute
    {
        new IEnumerable<IMenuItem<TAttr>> GetMenuItems(Type type);
    }
}
