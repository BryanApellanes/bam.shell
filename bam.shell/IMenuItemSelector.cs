using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuItemSelector
    {
        event EventHandler<MenuEventArgs> MenuItemSelectionChanged;
        IMenuItem? SelectMenuItem(IMenu menu, IMenuInput menuInput);
    }
}
