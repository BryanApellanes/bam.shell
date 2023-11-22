using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class MenuSpec<CType, IType> : MenuSpec
    {
        public MenuSpec()
        { 
            this.ContainerType = typeof(CType);
            this.ItemAttributeType = typeof(IType);
        }
    }
}
