using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// A typed argument provider that only supports string parameters, returning the input string directly.
    /// </summary>
    public class StringArgumentProvider : ITypedArgumentProvider
    {
        /// <summary>
        /// Returns the input string directly if the parameter type is <see cref="string"/>; otherwise throws.
        /// </summary>
        /// <param name="parameter">The parameter whose type must be <see cref="string"/>.</param>
        /// <param name="input">The string input to return as the argument.</param>
        /// <returns>The input string.</returns>
        /// <exception cref="ArgumentException">Thrown when the parameter type is not <see cref="string"/>.</exception>
        public object? GetTypedArgument(ParameterInfo parameter, string input)
        {
            if(parameter.ParameterType != typeof(string))
            {
                throw new ArgumentException($"This argument provider only supports strings: specified parameter type was {parameter.ParameterType.Name}");
            }

            return input;
        }
    }
}
