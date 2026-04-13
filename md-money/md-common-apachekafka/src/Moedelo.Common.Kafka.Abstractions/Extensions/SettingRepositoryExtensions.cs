using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Extensions
{
    internal static class SettingRepositoryExtensions
    {
        internal static SettingValue GetKafkaBrokerEndpoints(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("KafkaBrokerEndpoints");
        }

        internal static SettingValue GetKafkaUseMachineNameAsGroupIdPrefix(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("KafkaUseMachineNameAsGroupIdPrefix");
        }

        internal static SettingValue GetKafkaGroupIdPrefix(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("KafkaGroupIdPrefix");
        }

        internal static SettingValue GetKafkaConsumerFetchWaitMaxMs(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("KafkaConsumerFetchWaitMaxMs");
        }

        internal static SettingValue GetKafkaConsumerFetchMinBytes(this ISettingRepository settingRepository)
        {
            return settingRepository.Get("KafkaConsumerFetchMinBytes");
        }
    }
}