using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Kafka.Monitoring.Extensions;

public static class SettingRepositoryExtensions
{
    public static SettingValue GetConsumersConsulKeyPathSetting(this ISettingRepository settingRepository)
    {
        const string settingName = "ConsumersConsulKeyPath"; 
        const string defaultValue = "/runtime/kafka/consumers/";

        return settingRepository.Get(settingName, defaultValue);
    }

    public static SettingValue GetIsKafkaConsumersMonitoringEnabledSetting(this ISettingRepository settingRepository)
    {
        return settingRepository.Get("IsKafkaConsumersMonitoringEnabled", "false");
    }
}
