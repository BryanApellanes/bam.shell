using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Shell
{
    public interface IMenuItemRunner
    {
        IDependencyProvider DependencyProvider { get; }
        IMenuInputMethodArgumentProvider MethodArgumentProvider { get; set; }
        IMenuItemRunResult RunMenuItem(IMenuItem menuItem, IMenuInput menuInput);
    }
}
