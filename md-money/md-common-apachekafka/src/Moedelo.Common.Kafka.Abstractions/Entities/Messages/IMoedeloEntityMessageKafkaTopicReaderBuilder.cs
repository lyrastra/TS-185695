#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Messages;

public interface IMoedeloEntityMessageKafkaTopicReaderBuilder<out TMessageValue, out TBaseReaderBuilder>
    where TMessageValue : MoedeloKafkaMessageValueBase
{
    /// <summary>
    /// Выставить правила повторных попыток обработки сообщений.
    /// Повторная попытка делается, если текущая заканчивается с ошибкой (исключением).
    /// ВНИМАНИЕ: общее время обработки включая все паузы и время исполнения попыток
    /// должно укладываться в период, задаваемый <see cref="OptionalReadSettings.MaxPollIntervalMs"/> 
    /// </summary>
    /// <param name="settings">правила выполнения повторных попыток обработки</param>
    TBaseReaderBuilder WithRetry(ConsumerActionRetrySettings? settings = null);

    TBaseReaderBuilder WithDebugLogging();

    TBaseReaderBuilder OnFatalException(Func<Exception, Task> handler);

    void EnsureIsNotRunning();

    TBaseReaderBuilder OnException(
        Func<TMessageValue, Exception, Task> onException);

    TBaseReaderBuilder SkipIfUnknownType();
    TBaseReaderBuilder ThrowIfUnknownType();
    TBaseReaderBuilder WithFallbackOffset(
        KafkaConsumerConfig.AutoOffsetResetType autoOffsetResetType);
    TBaseReaderBuilder WithConsumerCount(int count);
    TBaseReaderBuilder WithAutoConsumersCount();
    TBaseReaderBuilder WithOptionalSettings(OptionalReadSettings settings);
    TBaseReaderBuilder AddSettingInjector(Action<KafkaConsumerSettings> injector);

    /// <summary>
    /// Зарегистрировать колбэк, который будет вызван при ошибке десериализации токена контекста исполнения 
    /// </summary>
    /// <param name="onTokenDeserializationFailed">колбэк</param>
    public TBaseReaderBuilder WithExecutionContextTokenFallback(
        Func<TMessageValue, Exception, ValueTask<string>> onTokenDeserializationFailed);
    Task RunAsync(string groupId, CancellationToken cancellationToken);
}
