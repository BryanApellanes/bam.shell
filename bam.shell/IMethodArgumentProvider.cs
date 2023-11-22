using System.Reflection;

namespace Bam.Shell
{
    public interface IMethodArgumentProvider
    {
        object?[] GetMethodArguments(MethodInfo methodInfo);
    }
}