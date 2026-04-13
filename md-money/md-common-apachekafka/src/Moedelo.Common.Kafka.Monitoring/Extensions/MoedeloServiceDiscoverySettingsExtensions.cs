using Moedelo.Common.Consul.ServiceDiscovery.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Monitoring.Extensions;

internal static class MoedeloServiceDiscoverySettingsExtensions
{
    private static readonly Lazy<string> HostPid = new (() => $"{Environment.MachineName}--{Environment.ProcessId}");
    
    internal static string BuildConsumerConsulKey<TEvent>(
        this IMoedeloServiceDiscoverySettings serviceDiscoverySettings,
        string consulConsumersDirectory,
        TEvent @event) where TEvent : IConsumerLifecycleEvent
    {
        return Path.Combine(
                consulConsumersDirectory,
                @event.GroupId,
                @event.Topic,
                HostPid.Value,
                serviceDiscoverySettings.ServiceId,
                @event.ConsumerGuid)
            .Replace('\\', '/');
    }
    
    internal static string BuildConsumerConsulKey(
        this IMoedeloServiceDiscoverySettings serviceDiscoverySettings,
        string consulConsumersDirectory,
        ConsumerPartitionSetOnPauseEvent @event)
    {
        var consumerBasePath = serviceDiscoverySettings
            .BuildConsumerConsulKey<IConsumerLifecycleEvent>(consulConsumersDirectory, @event);

        return Path.Combine(
                consumerBasePath,
                "partitionOnPause",
                @event.Partition.ToString())
            .Replace('\\', '/');;
    }
}
