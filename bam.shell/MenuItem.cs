using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Default implementation of <see cref="IMenuItem"/> representing a single selectable and executable menu entry.
    /// </summary>
    public class MenuItem : IMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        public MenuItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class with the specified method and attribute type.
        /// </summary>
        /// <param name="method">The method that this menu item invokes.</param>
        /// <param name="attributeType">The type of the attribute decorating the method.</param>
        public MenuItem(MethodInfo method, Type attributeType)
        {
            this.MethodInfo = method;
            this.AttributeType = attributeType;
            this.Attribute = method.GetCustomAttribute(attributeType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class with the specified instance, method, and attribute type.
        /// </summary>
        /// <param name="instance">The instance on which the method is invoked.</param>
        /// <param name="method">The method that this menu item invokes.</param>
        /// <param name="attributeType">The type of the attribute decorating the method.</param>
        public MenuItem(object instance, MethodInfo method, Type attributeType) : this(method, attributeType)
        {
            this.Instance = instance;
        }

        /// <summary>
        /// Gets or sets the attribute that decorates the menu item method.
        /// </summary>
        public Attribute? Attribute
        {
            get;
            set;
        }

        Type _attributeType;

        /// <summary>
        /// Gets or sets the type of the attribute that decorates the menu item method.
        /// </summary>
        public virtual Type? AttributeType
        {
            get
            {
                if(_attributeType == null && Attribute != null)
                {
                    _attributeType = Attribute.GetType();
                }
                return _attributeType;
            }
            set
            {
                _attributeType ??= value;
            }
        }

        /// <summary>
        /// Gets the selector string used to identify this menu item, derived from the attribute or method name.
        /// </summary>
        public string Selector
        {
            get
            {
                string selector = string.Empty;
                Attribute?.TryGetPropertyValue("Selector", MethodInfo.Name.CaseAcronym().ToLowerInvariant(), out selector);

                return selector;
            }
        }

        /// <summary>
        /// Gets the display name shown to the user, derived from the attribute or method name.
        /// </summary>
        public string DisplayName
        {
            get
            {
                string displayName = string.Empty;
                Attribute?.TryGetPropertyValue("DisplayName", MethodInfo.Name, out displayName);

                return string.IsNullOrEmpty(displayName) ? MethodInfo.Name : displayName;
            }
        }

        /// <summary>
        /// Gets or sets the method info for the method that this menu item invokes.
        /// </summary>
        public MethodInfo MethodInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this menu item is currently selected.
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the instance on which the menu item method is invoked.
        /// </summary>
        public object Instance
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
