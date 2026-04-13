using System;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IKafkaConsumerFactorySettings))]
internal sealed class KafkaConsumerFactorySettings : IKafkaConsumerFactorySettings
{
    private readonly EnumSettingValue<LogLevel> commitmentLogLevelSetting;
    private readonly EnumSettingValue<LogLevel> consumingLogLevelSetting;
    private readonly EnumSettingValue<LogLevel> consumedLogLevelSetting;
    private readonly IntSettingValue initPauseInMillisecondsSetting;

    public KafkaConsumerFactorySettings(ISettingRepository settingRepository)
    {
        // значения подобрано эмпирически
        const int defaultPauseInMilliseconds = 1000;
        
        initPauseInMillisecondsSetting = settingRepository
            .GetInt("KafkaMessagePauseBeforeStartFirstConsumerInMilliseconds", defaultPauseInMilliseconds);
        consumingLogLevelSetting = settingRepository
            .GetEnum("KafkaMessageConsumingLogLevel", LogLevel.Trace);
        consumedLogLevelSetting = settingRepository
            .GetEnum("KafkaMessageConsumedLogLevel", LogLevel.Trace);
        commitmentLogLevelSetting = settingRepository
            .GetEnum("KafkaMessageCommitmentLogLevel", LogLevel.Information);
    }

    public LogLevel CommitmentLogLevel => commitmentLogLevelSetting.Value;
    public LogLevel ConsumingLogLevel => consumingLogLevelSetting.Value;
    public LogLevel ConsumedLogLevel => consumedLogLevelSetting.Value;
    public TimeSpan PauseBeforeFirstConsumerStart => TimeSpan.FromMilliseconds(initPauseInMillisecondsSetting.Value);
}
