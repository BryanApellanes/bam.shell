using System.Reflection;

namespace Bam.Shell
{
    /// <summary>
    /// Defines the contract for providing arguments for a method invocation.
    /// </summary>
    public interface IMethodArgumentProvider
    {
        /// <summary>
        /// Gets the arguments for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method to resolve arguments for.</param>
        /// <returns>An array of arguments to pass to the method.</returns>
        object?[] GetMethodArguments(MethodInfo methodInfo);
    }
}