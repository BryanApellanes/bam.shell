using System.Reflection;
using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Shell;

/// <summary>
/// Provides typed arguments by resolving types from a <see cref="ServiceRegistry"/>. Currently not implemented.
/// </summary>
public class ServiceRegistryTypedArgumentProvider : ITypedArgumentProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceRegistryTypedArgumentProvider"/> class.
    /// </summary>
    /// <param name="serviceRegistry">The service registry to resolve types from.</param>
    public ServiceRegistryTypedArgumentProvider(ServiceRegistry serviceRegistry)
    {
        this.ServiceRegistry = serviceRegistry;
    }

    /// <summary>
    /// Gets the service registry used for type resolution.
    /// </summary>
    public ServiceRegistry ServiceRegistry
    {
        get;
        init;
    }

    /// <inheritdoc/>
    /// <exception cref="NotImplementedException">Always thrown; this method is not yet implemented.</exception>
    public object? GetTypedArgument(ParameterInfo parameter, string input)
    {
        throw new NotImplementedException();
    }
}