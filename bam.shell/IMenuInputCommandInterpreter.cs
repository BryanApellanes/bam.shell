using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuInputCommandInterpreter
    {
        /// <summary>
        /// Interpret the input.
        /// </summary>
        /// <param name="menuManager">The current menu manager.</param>
        /// <param name="menuInput">The input.</param>
        /// <param name="result">The result.</param>
        /// <returns>A boolean indicating if the input was interpreted.</returns>
        bool InterpretInput(IMenuManager menuManager, IMenuInput menuInput, out IInputCommandResults result);
        
    }
}
