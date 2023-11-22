using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IInputCommandResult
    {       
        string InputName { get; set; }
        object? InvocationResult { get; }
        bool Success { get; }
        string? Message { get; }
        Exception Exception { get; set; }
    }
}
