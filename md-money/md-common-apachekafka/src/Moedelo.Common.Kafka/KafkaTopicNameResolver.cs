using System;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IKafkaTopicNameResolver))]
internal sealed class KafkaTopicNameResolver : IKafkaTopicNameResolver
{
    private readonly SettingValue useMachineNameAsTopicPrefixSetting;
    private readonly SettingValue topicPrefixSetting;
    private readonly Lazy<string> lazyPrefix;

    public KafkaTopicNameResolver(ISettingRepository settingRepository)
    {
        useMachineNameAsTopicPrefixSetting = settingRepository.Get("KafkaUseMachineNameAsTopicPrefix");
        topicPrefixSetting = settingRepository.Get("KafkaTopicPrefix");
        // нет смысла персчитывать это значение в рантайме - достаточно вычислить один раз
        lazyPrefix = new Lazy<string>(CalculateTopicPrefix);
    }
        
    public string GetTopicFullName(string baseTopicName)
    {
        if (string.IsNullOrWhiteSpace(baseTopicName))
        {
            throw new ArgumentException("Пустое значение недопустимо", nameof(baseTopicName));
        }
            
        return $"{GetTopicPrefix()}.{baseTopicName}";
    }

    public string GetTopicPrefix() => lazyPrefix.Value;

    private string CalculateTopicPrefix()
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