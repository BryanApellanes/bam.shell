using Bam.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class DependencyProviderMethodArgumentProvider : IMethodArgumentProvider
    {
        public DependencyProviderMethodArgumentProvider(IDependencyProvider dependencyProvider)
        {
            this.DependencyProvider = dependencyProvider;
        }

        protected IDependencyProvider DependencyProvider
        {
            get;
            set;
        }

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
