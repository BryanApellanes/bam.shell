using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class NullMenuInputCommandInterpreter : IMenuInputCommandInterpreter
    {
        public bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IInputCommandResults result)
        {
            result = new InputCommandResults();
            return false;
        }
    }
}
