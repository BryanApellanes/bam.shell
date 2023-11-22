using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuInputReader
    {
        event EventHandler<MenuInputOutputLoopEventArgs> ReadingInput;
        IMenuInput ReadMenuInput();
    }
}
