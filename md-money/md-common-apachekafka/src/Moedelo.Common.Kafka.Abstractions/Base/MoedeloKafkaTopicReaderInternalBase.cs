using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Base;

public abstract class MoedeloKafkaTopicReaderInternalBase
{
    private readonly IKafkaConsumerStarter consumerStarter;
    private readonly IKafkaTopicNameResolver topicNameResolver;
    private readonly IKafkaConsumerBalancer kafkaConsumerBalancer;
    private readonly IKafkaConsumerGroupIdPrefixResolver groupIdPrefixResolver;
    private readonly ILogger logger;

    private readonly SettingValue brokerEndpointsSetting;
    private readonly SettingValue fetchWaitMaxMsSetting;
    private readonly SettingValue fetchMinBytesSetting;

    protected MoedeloKafkaTopicReaderInternalBase(
        IKafkaConsumerStarter consumerStarter,
        ISettingRepository settingRepository,
        IKafkaTopicNameResolver topicNameResolver,
        IKafkaConsumerBalancer kafkaConsumerBalancer,
        ILogger logger = null)
    {
        this.consumerStarter = consumerStarter;
        this.topicNameResolver = topicNameResolver;
        this.kafkaConsumerBalancer = kafkaConsumerBalancer;
        this.groupIdPrefixResolver = settingRepository.CreateConsumerGroupIdPrefixResolver();
        this.logger = logger;

        brokerEndpointsSetting = settingRepository.GetKafkaBrokerEndpoints();
        fetchWaitMaxMsSetting = settingRepository.GetKafkaConsumerFetchWaitMaxMs();
        fetchMinBytesSetting = settingRepository.GetKafkaConsumerFetchMinBytes();
    }

    private protected async Task ReadTopicImpl<TMessage>(
        ReadTopicSetting<TMessage> readSettings,
        Func<TMessage, Task> onMessage,
        CancellationToken cancellationToken) where TMessage : MoedeloKafkaMessageValueBase
    {
        InjectSettingsFromGlobalConfiguration(readSettings);
        readSettings.SetDefaultSettingsIfNotSet();

        var brokerEndpoints = brokerEndpointsSetting.Value;
        var consumerGroupId = new KafkaConsumerGroupId(groupIdPrefixResolver.GetGroupIdPrefix(), readSettings.GroupId);
        var topicName = topicNameResolver.GetKafkaTopicName(readSettings.TopicName); 
        var consumerConnectionSettings = readSettings.CreateConsumerConnectionSettings(brokerEndpoints, consumerGroupId, topicName);

        var onFatalException = readSettings.OnFatalException
                               ?? (exception => LogFatalException(consumerConnectionSettings, exception));

        if (readSettings.AutoConsumersCount)
        {
            var autoBalanceSettings = new ConsumerAutoBalanceSettings<TMessage>(
                consumerConnectionSettings, onMessage, onFatalException, readSettings.ConsumerFactoryType);

            await kafkaConsumerBalancer
                .StartAutoBalanceAsync(autoBalanceSettings, cancellationToken)
                .ConfigureAwait(false);
        }
        else
        {
            var consumerHandlers = new KafkaConsumerHandlers<TMessage>((message, _) => onMessage(message))
                .WithFatalExceptionHandler(onFatalException); 
            var consumerSettings = new KafkaConsumerSettings<TMessage>(
                consumerConnectionSettings, consumerHandlers, readSettings.ConsumerFactoryType);

            // стартуем консьюмеры. они завершаются по отзыву cancellationToken
            await Task
                .WhenAll(Enumerable
                    .Range(0, readSettings.ConsumerCount)
                    .Select(_ => consumerStarter.ListenAsync(consumerSettings, cancellationToken)))
                .ConfigureAwait(false);
        }
    }

    protected static void ValidateHandlerFunc(Delegate func, string funcName)
    {
        if (func == null)
        {
            throw new ArgumentNullException(funcName, $"Handler function with name {funcName} cannot be null");
        }
    }

    private void InjectSettingsFromGlobalConfiguration(KafkaConsumerSettings setting)
    {
        if (setting.FetchWaitMaxMs.HasValue == false && fetchWaitMaxMsSetting.IsNullOrEmpty() == false)
        {
            setting.FetchWaitMaxMs = fetchWaitMaxMsSetting.GetIntValueOrDefault(ReadTopicSettingDefaults.FetchWaitMaxMs);
        }

        if (setting.FetchMinBytes.HasValue == false && fetchMinBytesSetting.IsNullOrEmpty() == false)
        {
            setting.FetchMinBytes = fetchMinBytesSetting.GetIntValueOrDefault(ReadTopicSettingDefaults.FetchMinBytes);
        }
    }

    private Task LogFatalException(KafkaConsumerConfig settings, Exception fatalException)
    {
        logger?.LogError(
            fatalException,
            "{TopicName}: fatal Exception On ListenAsync",
            settings.TopicName.Raw);

        return Task.CompletedTask;
    }
}