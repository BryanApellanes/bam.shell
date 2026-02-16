using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for a single selectable and executable item within a menu.
    /// </summary>
    public interface IMenuItem
    {
        /// <summary>
        /// Gets or sets the instance on which the menu item method is invoked.
        /// </summary>
        object Instance { get; set; }

        /// <summary>
        /// Gets or sets the attribute that decorates the menu item method.
        /// </summary>
        Attribute? Attribute { get; set; }

        /// <summary>
        /// Gets or sets the type of the attribute that decorates the menu item method.
        /// </summary>
        Type? AttributeType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this menu item is currently selected.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// Gets the selector string used to identify this menu item.
        /// </summary>
        string Selector { get; }

        /// <summary>
        /// Gets the display name shown to the user for this menu item.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the method info for the method that this menu item invokes.
        /// </summary>
        MethodInfo MethodInfo { get; }
    }
}
