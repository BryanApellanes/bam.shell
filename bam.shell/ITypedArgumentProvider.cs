using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for converting string input into typed method arguments.
    /// </summary>
    public interface ITypedArgumentProvider
    {
        /// <summary>
        /// Converts the string input into an argument of the type specified by the parameter.
        /// </summary>
        /// <param name="parameter">The parameter whose type determines the conversion target.</param>
        /// <param name="input">The string input to convert.</param>
        /// <returns>The typed argument value, or null if conversion is not possible.</returns>
        object? GetTypedArgument(ParameterInfo parameter, string input);
    }
}
