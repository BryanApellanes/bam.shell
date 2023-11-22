using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuInputMethodArgumentProvider
    {
        ITypedArgumentProvider TypedArgumentProvider { get; }
        object?[] GetMethodArguments(IMenuItem menuItem, IMenuInput menuInput);
    }
}
