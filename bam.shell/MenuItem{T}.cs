using System.Reflection;

namespace Bam.Shell
{ 
    /// <summary>
    /// A strongly-typed menu item whose attribute is of the specified type.
    /// </summary>
    /// <typeparam name="TAttr">The attribute type decorating the menu item method.</typeparam>
    public class MenuItem<TAttr> : MenuItem, IMenuItem<TAttr> where TAttr : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem{TAttr}"/> class.
        /// </summary>
        public MenuItem() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem{TAttr}"/> class with the specified method.
        /// </summary>
        /// <param name="method">The method that this menu item invokes.</param>
        public MenuItem(MethodInfo method)
        {
            this.MethodInfo = method;
            this.Attribute = method.GetCustomAttribute<TAttr>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem{TAttr}"/> class with the specified instance and method.
        /// </summary>
        /// <param name="instance">The instance on which the method is invoked.</param>
        /// <param name="method">The method that this menu item invokes.</param>
        public MenuItem(object instance, MethodInfo method) : this(method)
        {
            this.Instance = instance;
        }

        TAttr? _attribute;

        /// <summary>
        /// Gets or sets the strongly-typed attribute that decorates the menu item method.
        /// </summary>
        public new TAttr? Attribute
        {
            get
            {
                return _attribute;
            }
            set
            {
                _attribute = value;
                base.Attribute = value;
            }
        }

        Attribute? IMenuItem.Attribute
        {
            get
            {
                return Attribute;
            }
            set
            {
                Attribute = (TAttr?)value;
            }
        }

        Type _attributeType;

        /// <inheritdoc/>
        public override Type? AttributeType
        {
            get
            {
                if (_attributeType == null && Attribute != null)
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
    }
}
