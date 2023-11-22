using Bam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public interface IMenuItemRunner
    {
        IDependencyProvider DependencyProvider { get; }
        IMenuInputMethodArgumentProvider MethodArgumentProvider { get; set; }
        IMenuItemRunResult RunMenuItem(IMenuItem menuItem, IMenuInput menuInput);
    }
}
