namespace Bam.Shell
{
    public interface IMenuItemProvider
    {
        IEnumerable<IMenuItem> GetMenuItems(object instance);
        IEnumerable<IMenuItem> GetMenuItems(Type containerType);
        IEnumerable<IMenuItem<T>> GetMenuItems<T>(Type containerType) where T : Attribute;
        IEnumerable<IMenuItem> GetMenuItems(Type containerType, Type itemAttributeType);
    }
}
