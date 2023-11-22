using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class MenuManagerUpdateStateEventArgs : EventArgs
    {
        public MenuManagerUpdateStateEventArgs() { }
        public IMenu Menu { get; set; }
        public IMenuInput MenuInput { get; set; }

        public IMenuItem? SelectedMenuItem
        {
            get
            {
                return Menu?.SelectedItem ?? null;
            }
        }
    }
}
