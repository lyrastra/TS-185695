using System;
using Moedelo.InfrastructureV2.ApacheKafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.InfrastructureV2.ApacheKafka.Implementations
{
    [InjectAsSingleton(typeof(IKafkaTopicNameResolver))]
    internal sealed class KafkaTopicNameResolver : IKafkaTopicNameResolver
    {
        private readonly SettingValue useMachineNameAsTopicPrefixSetting;
        private readonly SettingValue topicPrefixSetting;

        public KafkaTopicNameResolver(ISettingRepository settingRepository)
        {
            useMachineNameAsTopicPrefixSetting = settingRepository.Get("KafkaUseMachineNameAsTopicPrefix");
            topicPrefixSetting = settingRepository.Get("KafkaTopicPrefix");
        }
        
        public string GetTopicFullName(string baseTopicName)
        {
            if (string.IsNullOrWhiteSpace(baseTopicName))
            {
                throw new ArgumentException(nameof(baseTopicName));
            }
            
            return $"{GetTopicPrefix()}.{baseTopicName}";
        }

        private string GetTopicPrefix()
        {
            var prefix = topicPrefixSetting.Value;

            if (!string.IsNullOrEmpty(prefix))
            {
                return prefix;
            }

            var useMachineName = useMachineNameAsTopicPrefixSetting.GetBoolValueOrDefault(false);

            if (useMachineName)
            {
                return Environment.MachineName;
            }

            throw new Exception($"Парадокс настроек: ожидается, что либо выставлено непустое значение для настройки {topicPrefixSetting.Name}, " +
                                $"либо выставлено значение true для настройки {useMachineNameAsTopicPrefixSetting.Name}");
        }
    }
}
