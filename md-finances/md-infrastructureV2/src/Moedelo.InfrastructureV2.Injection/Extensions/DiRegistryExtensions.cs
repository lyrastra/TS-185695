using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Injection.Internals;

namespace Moedelo.InfrastructureV2.Injection.Extensions;

public static class DiRegistryExtensions
{
    public static IDiRegistry AddCreationCheck<TService>(this IDiRegistry diRegistry)
    {
        ServicesCreationCheck.AddTypeToCheck<TService>();

        return diRegistry;
    }
}
