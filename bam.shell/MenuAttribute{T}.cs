namespace Bam.Shell
{
    /// <summary>
    /// A strongly-typed menu attribute that specifies the item attribute type used to identify menu items.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type used to identify menu items.</typeparam>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MenuAttribute<TAttr> : MenuAttribute where TAttr : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAttribute{TAttr}"/> class.
        /// </summary>
        public MenuAttribute()
        {
            this.ItemAttributeType = typeof(TAttr);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAttribute{TAttr}"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the menu.</param>
        public MenuAttribute(string name): base(name)
        {
            this.ItemAttributeType = typeof(TAttr);
        }
    }
}
