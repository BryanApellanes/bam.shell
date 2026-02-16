namespace Bam.Shell
{
    /// <summary>
    /// Marks a class as a menu container, providing metadata such as name, description, and selector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MenuAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAttribute"/> class.
        /// </summary>
        public MenuAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAttribute"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name and display name of the menu.</param>
        public MenuAttribute(string name)
        {
            this.Name = name;
            this.DisplayName = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAttribute"/> class with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the menu.</param>
        /// <param name="description">The description of the menu.</param>
        public MenuAttribute(string name, string description) : this(name)
        {
            this.Description = description;
        }

        /// <summary>
        /// Gets the attribute type used to identify menu items within the decorated class.
        /// </summary>
        public Type ItemAttributeType { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name of the menu.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description of the menu.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the input command value used to select the associated menu.
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// Gets or sets the header text displayed above the menu.
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the footer text displayed below the menu.
        /// </summary>
        public string FooterText { get; set; }
    }
}
