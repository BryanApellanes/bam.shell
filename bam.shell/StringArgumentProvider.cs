using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Shell
{
    public class StringArgumentProvider : ITypedArgumentProvider
    {
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
