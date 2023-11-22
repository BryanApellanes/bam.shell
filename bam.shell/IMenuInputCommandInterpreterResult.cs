using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuInputCommandInterpreterResult
    {
        IEnumerable<IInputCommandResult?> MenuItemRunResults { get; }

        MenuInputCommandInterpreterResult AddResult(object? result);
        MenuInputCommandInterpreterResult AddResult(IInputCommandResult? result);
    }
}
