using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class MenuInputOutputLoopEventArgs : EventArgs
    {
        public MenuInputOutputLoopEventArgs() { }
        public IMenuInputOutputLoop MenuInputOutputLoop { get; set; }
        public IMenuInputReader MenuInputReader { get; set; }
        public IMenuInput MenuInput { get; set; }
    }
}
