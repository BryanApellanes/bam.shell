namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for discovering and providing menu items from container types.
    /// </summary>
    public interface IMenuItemProvider
    {
        /// <summary>
        /// Gets menu items from the specified instance's type, assigning the instance for invocation.
        /// </summary>
        /// <param name="instance">The instance to discover menu items from.</param>
        /// <returns>An enumerable of menu items.</returns>
        IEnumerable<IMenuItem> GetMenuItems(object instance);

        /// <summary>
        /// Gets menu items from the specified container type.
        /// </summary>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <returns>An enumerable of menu items.</returns>
        IEnumerable<IMenuItem> GetMenuItems(Type containerType);

        /// <summary>
        /// Gets strongly-typed menu items from the specified container type.
        /// </summary>
        /// <typeparam name="T">The attribute type identifying menu items.</typeparam>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <returns>An enumerable of strongly-typed menu items.</returns>
        IEnumerable<IMenuItem<T>> GetMenuItems<T>(Type containerType) where T : Attribute;

        /// <summary>
        /// Gets menu items from the specified container type using the specified item attribute type.
        /// </summary>
        /// <param name="containerType">The type to discover menu items from.</param>
        /// <param name="itemAttributeType">The attribute type identifying menu items.</param>
        /// <returns>An enumerable of menu items.</returns>
        IEnumerable<IMenuItem> GetMenuItems(Type containerType, Type itemAttributeType);
    }
}
