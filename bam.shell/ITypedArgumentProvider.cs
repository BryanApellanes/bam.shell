using System.Reflection;

namespace Bam.Shell
{
    public interface ITypedArgumentProvider
    {
        object? GetTypedArgument(ParameterInfo parameter, string input);
    }
}
