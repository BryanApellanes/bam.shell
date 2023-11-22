using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuItemProvider<TAttr> : IMenuItemProvider where TAttr : Attribute
    {
        new IEnumerable<IMenuItem<TAttr>> GetMenuItems(Type type);
    }
}
