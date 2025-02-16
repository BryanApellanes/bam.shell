namespace Bam.Shell
{
    public class MenuItemRunEventArgs : EventArgs
    {
        public MenuItemRunEventArgs() { }

        public IMenu Menu { get; set; }
        public IMenuItem MenuItem { get; set; }
        public IMenuInput MenuInput { get; set; }
        public IMenuItemRunResult Result { get; set; }
    }
}
