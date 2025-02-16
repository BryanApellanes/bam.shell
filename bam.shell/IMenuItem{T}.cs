namespace Bam.Shell
{
    public interface IMenuItem<T>: IMenuItem where T: Attribute
    {
        new T? Attribute { get; set; }
    }
}
