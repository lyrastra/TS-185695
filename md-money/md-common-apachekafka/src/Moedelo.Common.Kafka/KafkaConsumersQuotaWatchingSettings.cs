using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IKafkaConsumersQuotaWatchingSettings))]
internal sealed class KafkaConsumersQuotaWatchingSettings : IKafkaConsumersQuotaWatchingSettings
{
    private readonly ISettingsConfigurations settingsConfigurations;

    public KafkaConsumersQuotaWatchingSettings(ISettingsConfigurations settingsConfigurations)
    {
        this.settingsConfigurations = settingsConfigurations;
    }

    public string ConsulTopicsKeyValueRootDirectory =>
        $"{settingsConfigurations.Config.Environment}/runtime/kafka/topics";
}
