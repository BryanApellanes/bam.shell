using Bam.Net;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{ 
    public class MenuItem<TAttr> : MenuItem, IMenuItem<TAttr> where TAttr : Attribute
    {
        public MenuItem() { }

        public MenuItem(MethodInfo method)
        {
            this.MethodInfo = method;
            this.Attribute = method.GetCustomAttribute<TAttr>();
        }

        public MenuItem(object instance, MethodInfo method) : this(method)
        {
            this.Instance = instance;
        }

        TAttr? _attribute;
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
