using System;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Base;

internal sealed class KafkaConsumerGroupIdPrefixResolver : IKafkaConsumerGroupIdPrefixResolver
{
    private readonly SettingValue useMachineNameAsGroupIdSetting;
    private readonly SettingValue groupIdPrefixSetting;

    public KafkaConsumerGroupIdPrefixResolver(ISettingRepository settingRepository)
    {
        useMachineNameAsGroupIdSetting = settingRepository.GetKafkaUseMachineNameAsGroupIdPrefix();
        groupIdPrefixSetting = settingRepository.GetKafkaGroupIdPrefix();
    }

    public string GetGroupIdPrefix()
    {
        var prefix = groupIdPrefixSetting.Value;

        if (!string.IsNullOrEmpty(prefix))
        {
            return prefix;
        }

        var useMachineName = useMachineNameAsGroupIdSetting.GetBoolValueOrDefault(false);

        if (useMachineName)
        {
            return Environment.MachineName;
        }

        throw new Exception($"Парадокс настроек: ожидается, что либо выставлено непустое значение для настройки {groupIdPrefixSetting.Name}, " +
                            $"либо выставлено значение true для настройки {useMachineNameAsGroupIdSetting.Name}");
    }
}
