using System.Reflection;
using Bam.CoreServices;

namespace Bam.Shell;

public class ServiceRegistryTypedArgumentProvider : ITypedArgumentProvider
{
    public ServiceRegistryTypedArgumentProvider(ServiceRegistry serviceRegistry)
    {
        this.ServiceRegistry = serviceRegistry;
    }

    public ServiceRegistry ServiceRegistry
    {
        get;
        init;
    }
    
    public object? GetTypedArgument(ParameterInfo parameter, string input)
    {
        throw new NotImplementedException();
    }
}