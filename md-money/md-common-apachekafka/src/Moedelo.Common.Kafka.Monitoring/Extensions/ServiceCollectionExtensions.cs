using Microsoft.Extensions.DependencyInjection;
using Moedelo.Common.Kafka.Monitoring.HostedServices;

namespace Moedelo.Common.Kafka.Monitoring.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMoedeloKafkaConsumersMonitoring(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddHostedService<KafkaTopicConsumersWatcherHostedService>();
    }
}
