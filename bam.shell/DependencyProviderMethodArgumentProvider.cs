using Bam.Services;
using System.Reflection;
using Bam.DependencyInjection;

namespace Bam.Shell
{
    /// <summary>
    /// Provides method arguments by resolving parameter types from a dependency provider.
    /// </summary>
    public class DependencyProviderMethodArgumentProvider : IMethodArgumentProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyProviderMethodArgumentProvider"/> class.
        /// </summary>
        /// <param name="dependencyProvider">The dependency provider used to resolve parameter types.</param>
        public DependencyProviderMethodArgumentProvider(IDependencyProvider dependencyProvider)
        {
            this.DependencyProvider = dependencyProvider;
        }

        /// <summary>
        /// Gets or sets the dependency provider used to resolve parameter types.
        /// </summary>
        protected IDependencyProvider DependencyProvider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the method arguments by resolving each parameter type from the dependency provider.
        /// </summary>
        /// <param name="methodInfo">The method to resolve arguments for.</param>
        /// <returns>An array of resolved arguments, or an empty array if the method has no parameters.</returns>
        public object?[] GetMethodArguments(MethodInfo methodInfo)
        {
            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            if (parameterInfos.Length == 0)
            {
                return Array.Empty<object>();
            }

            object?[] arguments = new object[parameterInfos.Length];
            for (int i = 0; i < parameterInfos.Length; i++)
            {
                arguments[i] = DependencyProvider.Get(parameterInfos[i].ParameterType);
            }

            return arguments;
        }
    }
}
