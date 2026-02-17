namespace Bam.Shell
{
    /// <summary>
    /// Describes a menu by its container type and item attribute type.
    /// </summary>
    public class MenuSpec
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpec"/> class.
        /// </summary>
        public MenuSpec() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpec"/> class with the specified container type, defaulting to <see cref="MenuItemAttribute"/>.
        /// </summary>
        /// <param name="containerType">The type that contains menu item methods.</param>
        public MenuSpec(Type containerType) : this(containerType, typeof(MenuItemAttribute))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuSpec"/> class with the specified container type and item attribute type.
        /// </summary>
        /// <param name="containerType">The type that contains menu item methods.</param>
        /// <param name="itemAttributeType">The attribute type used to identify menu items.</param>
        public MenuSpec(Type containerType, Type itemAttributeType)
        {
            this.ContainerType  = containerType;
            this.ItemAttributeType = itemAttributeType;
        }

        /// <summary>
        /// Gets or sets the type that contains menu item methods.
        /// </summary>
        public Type ContainerType { get; set; } = null!;

        /// <summary>
        /// Gets or sets the attribute type used to identify menu items.
        /// </summary>
        public Type ItemAttributeType { get; set; } = null!;

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj == this) return true;
            if(obj is MenuSpec menuSpec)
            {
                return menuSpec.ContainerType == ContainerType && menuSpec.ItemAttributeType == ItemAttributeType;
            }
            return false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                if(ContainerType != null)
                {
                    hash = hash * 23 + ContainerType.GetHashCode();
                }
                if(ItemAttributeType != null)
                {
                    hash = hash * 23 + ItemAttributeType.GetHashCode();
                }
                return hash;
            }
        }
    }
}
