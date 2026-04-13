using Microsoft.Extensions.Hosting;
using Moedelo.Common.Kafka.Monitoring.Abstractions;
using Moedelo.Common.Kafka.Monitoring.Extensions;

namespace Moedelo.Common.Kafka.Monitoring.HostedServices;

internal class KafkaTopicConsumersWatcherHostedService : BackgroundService
{
    // вся работа в KafkaTopicConsumersWatcher производится в конструкторе и "деструкторе"
    // ReSharper disable once NotAccessedField.Local
    private readonly IKafkaTopicConsumersWatcher watcher;

    public KafkaTopicConsumersWatcherHostedService(IKafkaTopicConsumersWatcher watcher)
    {
        this.watcher = watcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await stoppingToken;
    }
}
