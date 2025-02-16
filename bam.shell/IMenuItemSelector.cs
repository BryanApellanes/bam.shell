namespace Bam.Shell
{
    public interface IMenuItemSelector
    {
        event EventHandler<MenuEventArgs> MenuItemSelectionChanged;
        IMenuItem? SelectMenuItem(IMenu menu, IMenuInput menuInput);
    }
}
