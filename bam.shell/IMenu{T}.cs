namespace Bam.Shell
{
    public interface IMenu<TAttr>: IMenu where TAttr : Attribute
    {
        new IEnumerable<IMenuItem<TAttr>> Items { get; }
    }
}
