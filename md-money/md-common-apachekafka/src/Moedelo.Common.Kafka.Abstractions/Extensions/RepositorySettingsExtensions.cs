using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

public static class RepositorySettingsExtensions
{
    public static IKafkaConsumerGroupIdPrefixResolver CreateConsumerGroupIdPrefixResolver(
        this ISettingRepository settingRepository)
    {
        return new KafkaConsumerGroupIdPrefixResolver(settingRepository);
    }

    public static SettingValue GetConsumersConsulKeyPathSetting(this ISettingRepository settingRepository)
    {
        const string settingName = "ConsumersConsulKeyPath"; 
        const string defaultValue = "/runtime/kafka/consumers/";

        return settingRepository.Get(settingName, defaultValue);
    }
}
