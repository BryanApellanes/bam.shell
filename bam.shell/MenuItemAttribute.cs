namespace Bam.Shell
{
    /// <summary>
    /// Used to adorn a method that is included in a menu.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MenuItemAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemAttribute"/> class.
        /// </summary>
        public MenuItemAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemAttribute"/> class with the specified display name.
        /// </summary>
        /// <param name="displayName">The display name of the menu item.</param>
        public MenuItemAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemAttribute"/> class with the specified display name and description.
        /// </summary>
        /// <param name="displayName">The display name of the menu item.</param>
        /// <param name="description">The description of the menu item.</param>
        public MenuItemAttribute(string displayName, string description) : this(displayName)
        {
            this.Description = description;
        }

        /// <summary>
        /// Gets or sets the selector string used to identify this menu item.
        /// </summary>
        public string Selector { get; set; } = null!;

        /// <summary>
        /// Gets or sets the display name shown to the user.
        /// </summary>
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of the menu item.
        /// </summary>
        public string Description { get; set; } = null!;
    }
}
