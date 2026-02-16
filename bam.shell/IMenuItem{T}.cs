namespace Bam.Shell
{
    /// <summary>
    /// Defines a strongly-typed menu item whose attribute is of the specified type.
    /// </summary>
    /// <typeparam name="T">The attribute type decorating the menu item method.</typeparam>
    public interface IMenuItem<T>: IMenuItem where T: Attribute
    {
        /// <summary>
        /// Gets or sets the strongly-typed attribute that decorates the menu item method.
        /// </summary>
        new T? Attribute { get; set; }
    }
}
