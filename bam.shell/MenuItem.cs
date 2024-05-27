using Bam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class MenuItem : IMenuItem
    {
        public MenuItem() { }

        public MenuItem(MethodInfo method, Type attributeType)
        {
            this.MethodInfo = method;
            this.AttributeType = attributeType;
            this.Attribute = method.GetCustomAttribute(attributeType);
        }

        public MenuItem(object instance, MethodInfo method, Type attributeType) : this(method, attributeType)
        {
            this.Instance = instance;
        }

        public Attribute? Attribute
        {
            get;
            set;
        }

        Type _attributeType;
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

        public string Selector
        {
            get
            {
                string selector = string.Empty;
                Attribute?.TryGetPropertyValue("Selector", MethodInfo.Name.CaseAcronym().ToLowerInvariant(), out selector);

                return selector;
            }
        }

        public string DisplayName
        {
            get
            {
                string displayName = string.Empty;
                Attribute?.TryGetPropertyValue("DisplayName", MethodInfo.Name, out displayName);

                return string.IsNullOrEmpty(displayName) ? MethodInfo.Name : displayName;
            }
        }

        public MethodInfo MethodInfo
        {
            get;
            set;
        }

        public bool Selected
        {
            get;
            set;
        }

        public object Instance
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
