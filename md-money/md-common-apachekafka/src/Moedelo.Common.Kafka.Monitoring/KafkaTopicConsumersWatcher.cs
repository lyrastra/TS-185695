using Moedelo.Common.Kafka.Monitoring.Abstractions;
using Moedelo.Common.Kafka.Monitoring.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Monitoring;

[InjectAsSingleton(typeof(IKafkaTopicConsumersWatcher))]
internal sealed class KafkaTopicConsumersWatcher : IKafkaTopicConsumersWatcher, IDisposable
{
    private readonly IKafkaConsumerStarter consumerStarter;
    private readonly ISettingsConfigurations settingsConfigurations;
    private readonly SettingValue consulConsumersPathSetting;
    private readonly SettingValue isConsumersMonitoringOn;
    private readonly IKafkaConsulApi kafkaConsulApi;

    public KafkaTopicConsumersWatcher(
        IKafkaConsumerStarter consumerStarter,
        ISettingRepository settingRepository,
        ISettingsConfigurations settingsConfigurations,
        IKafkaConsulApi kafkaConsulApi)
    {
        this.consumerStarter = consumerStarter;
        this.settingsConfigurations = settingsConfigurations;
        this.kafkaConsulApi = kafkaConsulApi;
        this.consulConsumersPathSetting = settingRepository.GetConsumersConsulKeyPathSetting();
        this.isConsumersMonitoringOn = settingRepository.GetIsKafkaConsumersMonitoringEnabledSetting();

        consumerStarter.ConsumerStartedEvent += OnConsumerStartedAsync;
        consumerStarter.ConsumerStoppedEvent += OnConsumerStoppedAsync;
        consumerStarter.PartitionSetOnPauseEvent += OnPartitionSetOnPauseAsync;
    }

    private string ConsumersConsulDirectory => Path.Combine(
            settingsConfigurations.Config.Environment,
            consulConsumersPathSetting.Value.TrimStart('/'))
        .Replace('\\', '/');

    public void Dispose()
    {
        consumerStarter.ConsumerStartedEvent -= OnConsumerStartedAsync;
        consumerStarter.ConsumerStoppedEvent -= OnConsumerStoppedAsync;
        consumerStarter.PartitionSetOnPauseEvent -= OnPartitionSetOnPauseAsync;
    }

    private async Task OnConsumerStartedAsync(ConsumerStartedEvent @event, CancellationToken cancellation)
    {
        if (isConsumersMonitoringOn.GetBoolValue() == false)
        {
            return;
        }

        await kafkaConsulApi.NotifyAboutConsumerStartedAsync(ConsumersConsulDirectory, @event, cancellation);
    }
    
    private async Task OnConsumerStoppedAsync(ConsumerStoppedEvent @event, CancellationToken cancellation)
    {
        if (isConsumersMonitoringOn.GetBoolValue() == false)
        {
            return;
        }

        await kafkaConsulApi.NotifyAboutConsumerStoppedAsync(ConsumersConsulDirectory, @event, cancellation);
    }

    private async Task OnPartitionSetOnPauseAsync(ConsumerPartitionSetOnPauseEvent @event, CancellationToken cancellation)
    {
        if (isConsumersMonitoringOn.GetBoolValue() == false)
        {
            return;
        }

        await kafkaConsulApi.NotifyAboutConsumerSetPartitionOnPauseAsync(ConsumersConsulDirectory, @event, cancellation);
    }
}
