namespace Bam.Shell
{
    public class MenuEventArgs : EventArgs
    {
        public MenuEventArgs() { }
        public IMenu Menu { get; set; }
        public IMenuItem? PreviousMenuItem { get; set; }
        public IMenuItem? MenuItem { get; set; }
    }
}
