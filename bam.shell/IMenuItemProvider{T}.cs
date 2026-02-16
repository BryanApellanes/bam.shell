namespace Bam.Shell
{
    /// <summary>
    /// Defines a strongly-typed contract for providing menu items identified by the specified attribute type.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type identifying menu items.</typeparam>
    public interface IMenuItemProvider<TAttr> : IMenuItemProvider where TAttr : Attribute
    {
        /// <summary>
        /// Gets strongly-typed menu items from the specified container type.
        /// </summary>
        /// <param name="type">The container type to discover menu items from.</param>
        /// <returns>An enumerable of strongly-typed menu items.</returns>
        new IEnumerable<IMenuItem<TAttr>> GetMenuItems(Type type);
    }
}
