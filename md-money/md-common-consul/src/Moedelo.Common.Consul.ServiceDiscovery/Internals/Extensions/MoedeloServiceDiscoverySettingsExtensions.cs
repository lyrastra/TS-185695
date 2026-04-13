using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;
using Moedelo.Infrastructure.Consul.Abstraction.Models;

namespace Moedelo.Common.Consul.ServiceDiscovery.Internals.Extensions;

internal static class MoedeloServiceDiscoverySettingsExtensions
{
    internal static AgentServiceRegistration CreateAgentServiceRegistration(
        this IMoedeloServiceDiscoverySettings settings,
        Uri listenerAddress, string dtFormat)
    {
        return new AgentServiceRegistration
        {
            ID = settings.ServiceId,
            Name = settings.ServiceName,
            Address = $"{listenerAddress.Scheme}://{listenerAddress.Host}",
            Port = listenerAddress.Port,
            Tags = [Environment.MachineName, settings.Domain, settings.AppName],
            Check = new AgentServiceRegistration.TtlCheckRegistration
            {
                Name = $"{settings.ServiceName}: Self Health Status",
                Notes = $"registered at {DateTime.Now.ToString(dtFormat)}",
                TTL = settings.ServiceTtl,
                DeregisterCriticalServiceAfter = settings.ServiceDeregistrationTimeout
            }
        };
    }
}
