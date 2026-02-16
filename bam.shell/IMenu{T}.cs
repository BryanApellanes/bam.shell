namespace Bam.Shell
{
    /// <summary>
    /// Defines a strongly-typed menu whose items are identified by the specified attribute type.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
    public interface IMenu<TAttr>: IMenu where TAttr : Attribute
    {
        /// <summary>
        /// Gets the collection of strongly-typed menu items.
        /// </summary>
        new IEnumerable<IMenuItem<TAttr>> Items { get; }
    }
}
