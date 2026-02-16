namespace Bam.Shell
{
    /// <summary>
    /// A strongly-typed <see cref="MenuSpec"/> that specifies the container type and item attribute type via generic type parameters.
    /// </summary>
    /// <typeparam name="CType">The container type that holds menu item methods.</typeparam>
    /// <typeparam name="IType">The attribute type used to identify menu items.</typeparam>
    public class MenuSpec<CType, IType> : MenuSpec
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpec{CType, IType}"/> class.
        /// </summary>
        public MenuSpec()
        { 
            this.ContainerType = typeof(CType);
            this.ItemAttributeType = typeof(IType);
        }
    }
}
