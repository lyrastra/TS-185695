using Microsoft.Extensions.DependencyInjection;
using Moedelo.Common.Consul.ServiceDiscovery.Internals;

namespace Moedelo.Common.Consul.ServiceDiscovery.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMoedeloServiceDiscovery(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddHostedService<MoedeloConsulServiceDiscoveryHostedService>();
    }
}
