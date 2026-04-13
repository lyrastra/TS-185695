#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Messages;

public abstract class MoedeloEntityMessageKafkaTopicReaderBuilder<TDefinition, TMessageValue, TMessageData,
    TBaseReaderBuilder> : IMoedeloEntityMessageKafkaTopicReaderBuilder<TMessageValue, TBaseReaderBuilder> where TMessageValue : MoedeloKafkaMessageValueBase
{
    protected readonly ILogger? logger;
    private readonly IMoedeloEntityMessageKafkaTopicReader<TMessageValue> reader;

    private bool isRunning;
    private readonly string topicName;
    private readonly string entityType;
    private UnknownMessageTypeStrategy unknownMessageTypeStrategy = UnknownMessageTypeStrategy.Undef;
    private bool withRetry;
    private bool withDebugLogging;
    private bool autoBalance;
    private ConsumerActionRetrySettings? retrySettings;
    private Func<TMessageValue, Exception, Task>? onEventException;
    private Func<Exception, Task>? onFatalException;
    private KafkaConsumerConfig.AutoOffsetResetType? offsetResetType;
    private int? consumerCount;
    private OptionalReadSettings? optionalReadSettings;
    private bool? useExecutionContext;
    private readonly Dictionary<string, MessageHandlerRegistration> messageHandlerRegistrations = new();
    private readonly List<Action<KafkaConsumerSettings>> settingsInjectors = new ();
    private Func<TMessageValue, Exception, ValueTask<string>>? executionContextTokenFallback;

    protected MoedeloEntityMessageKafkaTopicReaderBuilder(
        string topicName,
        string entityType,
        IMoedeloEntityMessageKafkaTopicReader<TMessageValue> reader,
        ILogger? logger)
    {
        this.topicName = topicName.EnsureIsNotNullOrWhiteSpace(nameof(topicName));
        this.entityType = entityType.EnsureIsNotNullOrWhiteSpace(nameof(entityType));
        this.reader = reader.EnsureIsNotNull(nameof(reader));
        this.logger = logger;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnsureIsNotRunning()
    {
        if (isRunning)
        {
            throw new InvalidOperationException($"{GetType().Name} is already running");
        }
    }

    private readonly record struct MessageHandlerRegistration(
        Func<TMessageValue, Task> OnMessage,
        Action<TDefinition>? DefinitionCustomizer);

    protected void OnMessage<TMessage>(
        Func<TMessage, Task> onMessage,
        Action<TDefinition>? definitionCustomizer = null)
        where TMessage : TMessageData
    {
        EnsureIsNotRunning();

        var key = GetMessageTypeName<TMessage>();
        var registration = new MessageHandlerRegistration(
            WrapMessage(onMessage.EnsureIsNotNull(nameof(onMessage))),
            definitionCustomizer);

        EnsureIsFirstRegistration<TMessage>(key);

        messageHandlerRegistrations.Add(key, registration);
    }

    private void EnsureIsFirstRegistration<TMessage>(string key) where TMessage : TMessageData
    {
        if (messageHandlerRegistrations.ContainsKey(key))
        {
            logger?.LogError("Обработчик для {Key} по типу {MessageTypeName} уже зарегистрирован, повторная регистрация недопустима", 
                key, typeof(TMessage).Name);

            throw new Exception($"Обработчик для {key} по типу {typeof(TMessage).Name} уже зарегистрирован, повторная регистрация недопустима");
        }
    }

    protected void OnMessage<TMessage>(
        Func<TMessage, KafkaMessageValueMetadata, Task> onMessage,
        Action<TDefinition>? definitionCustomizer = null)
        where TMessage : TMessageData
    {
        EnsureIsNotRunning();

        var key = GetMessageTypeName<TMessage>();
        var registration = new MessageHandlerRegistration(
            WrapMessage(onMessage.EnsureIsNotNull(nameof(onMessage))),
            definitionCustomizer);

        EnsureIsFirstRegistration<TMessage>(key);

        messageHandlerRegistrations.Add(key, registration);
    }

    public TBaseReaderBuilder OnException(
        Func<TMessageValue, Exception, Task> onException)
    {
        EnsureIsNotRunning();
        onEventException = onException;

        return Self;
    }

    public TBaseReaderBuilder OnFatalException(Func<Exception, Task> handler)
    {
        EnsureIsNotRunning();
        onFatalException = handler;

        return Self;
    }

    public TBaseReaderBuilder SkipIfUnknownType()
    {
        EnsureIsNotRunning();
        unknownMessageTypeStrategy = UnknownMessageTypeStrategy.Skip;

        return Self;
    }

    public TBaseReaderBuilder ThrowIfUnknownType()
    {
        EnsureIsNotRunning();
        unknownMessageTypeStrategy = UnknownMessageTypeStrategy.Throw;

        return Self;
    }

    public TBaseReaderBuilder WithRetry(ConsumerActionRetrySettings? settings = null)
    {
        EnsureIsNotRunning();
        withRetry = true;
        retrySettings = settings;

        return Self;
    }

    public TBaseReaderBuilder WithDebugLogging()
    {
        EnsureIsNotRunning();
        withDebugLogging = true;

        return Self;
    }

    public TBaseReaderBuilder WithFallbackOffset(
        KafkaConsumerConfig.AutoOffsetResetType autoOffsetResetType)
    {
        EnsureIsNotRunning();
        offsetResetType = autoOffsetResetType;

        return Self;
    }

    public TBaseReaderBuilder WithConsumerCount(int count)
    {
        EnsureIsNotRunning();

        if (count < 1)
        {
            throw new ArgumentException("Значение не может быть меньше 1", nameof(count));
        }

        consumerCount = count;
        autoBalance = false;

        return Self;
    }

    public TBaseReaderBuilder WithAutoConsumersCount()
    {
        EnsureIsNotRunning();
        Debug.Assert(consumerCount == null, $"Попытка выставить {nameof(WithAutoConsumersCount)} одновременно с {nameof(WithConsumerCount)}");

        autoBalance = true;
        consumerCount = null;

        return Self;
    }

    public TBaseReaderBuilder WithOptionalSettings(OptionalReadSettings settings)
    {
        EnsureIsNotRunning();
        optionalReadSettings = settings;

        return Self;
    }

    /// <summary>
    /// Не инициализировать execution context на уровне топика.
    /// По умолчанию при чтении из топика до определения конкретного типа полученного сообщения делается попытка
    /// извлечь из него информацию о контексте исполнения и установить его.
    /// Таким образом, обработка каждого сообщения из топика проходит в контексте исполнения, извлечённом из данного сообщения.
    /// Если вызвать данный метод, то информация о контексте исполнения извлекаться не будет и контекст исполнения создаваться не будет.
    /// Поведение для каждого типа сообщений, читаемых из топика, можно настроить
    /// с помощью <see cref="IMoedeloEntityMessageHandlerDefinition.WithoutContext"/>
    /// </summary>
    protected TBaseReaderBuilder WithoutExecutionContext()
    {
        EnsureIsNotRunning();
        useExecutionContext = false;

        return Self;
    }
    
    /// <summary>
    /// Зарегистрировать колбэк, который будет вызван при ошибке десериализации токена контекста исполнения 
    /// </summary>
    /// <param name="onTokenDeserializationFailed">колбэк</param>
    public TBaseReaderBuilder WithExecutionContextTokenFallback(
        Func<TMessageValue, Exception, ValueTask<string>> onTokenDeserializationFailed)
    {
        EnsureIsNotRunning();
        this.executionContextTokenFallback = onTokenDeserializationFailed;

        return Self;
    }

    private void EnsureCanBeRun(string groupId)
    {
        try
        {
            groupId.EnsureIsNotNullOrWhiteSpace(nameof(groupId));

            if (unknownMessageTypeStrategy == UnknownMessageTypeStrategy.Undef)
            {
                throw new Exception(
                    $"{nameof(UnknownMessageTypeStrategy)} = Undef, use {nameof(SkipIfUnknownType)} or {nameof(ThrowIfUnknownType)} to set value");
            }

            if (offsetResetType == null)
            {
                throw new Exception($"{nameof(offsetResetType)} is null, use {nameof(WithFallbackOffset)} to set value");
            }

            if (consumerCount == null && !autoBalance)
            {
                throw new Exception("ConsumerCount is null, use WithConsumerCount to set value");
            }

            if (executionContextTokenFallback != null && useExecutionContext == false)
            {
                throw new Exception("UseExecutionContext is false, but executionContextTokenFallback is not null");
            }
        }
        catch (Exception exception)
        {
            logger?.LogUnableToStartConsumerError(exception, GetType().Name);
            throw;
        }
    }

    public TBaseReaderBuilder AddSettingInjector(Action<KafkaConsumerSettings> injector)
    {
        this.settingsInjectors.Add(injector);

        return Self;
    }

    public Task RunAsync(string groupId, CancellationToken cancellationToken)
    {
        EnsureIsNotRunning();
        EnsureCanBeRun(groupId);
        isRunning = true;

        var offsetResetTypeValue = offsetResetType!.Value;
        var messageHandler = BuildOnMessageHandler(cancellationToken); 

        var readTopicSettings = new ReadTopicSetting<TMessageValue>(
            groupId, topicName, messageHandler, offsetResetTypeValue, consumerCount)
        {
            OnMessageException = onEventException,
            OnFatalException = onFatalException
        };

        readTopicSettings.SetOptionalSettings(optionalReadSettings);

        if (useExecutionContext.HasValue)
        {
            readTopicSettings.UseContext = useExecutionContext.Value;
        }

        foreach (var injector in settingsInjectors)
        {
            injector.Invoke(readTopicSettings);
        }

        return reader.ReadFromTopicAsync(readTopicSettings, cancellationToken);
    }

    private Func<TMessageValue, Task> BuildOnMessageHandler(
        CancellationToken cancellationToken)
    {
        var handlersMap = BuildHandlersMap(cancellationToken);

        return messageValue =>
        {
            var messageEntityType = GetMessageEntityType(messageValue); 

            if (messageEntityType != entityType)
            {
                logger?.LogUnexpectedMessageType(messageEntityType, entityType);

                throw new Exception($"messageValue.EntityType != entityType ({messageEntityType} != {entityType})");
            }

            var messageType = GetMessageType(messageValue);

            if (handlersMap.TryGetValue(messageType, out var handler) == false)
            {
                switch (unknownMessageTypeStrategy)
                {
                    case UnknownMessageTypeStrategy.Skip:
                        logger?.LogSkipUnexpectedMessageType(topicName, messageType);
                        return Task.CompletedTask;
                    case UnknownMessageTypeStrategy.Throw:
                        return Task.FromException(new Exception($"Тип сообщения не поддерживается: {messageType}"));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(unknownMessageTypeStrategy), unknownMessageTypeStrategy, "Неизвестная стратегия");
                }
            }

            return handler(messageValue);
        };
    }

    protected readonly record struct DefinitionSetup(
        Func<TMessageValue, Task> MessageHandler,
        string TopicName,
        bool WithRetry,
        ConsumerActionRetrySettings? RetrySettings,
        bool WithDebugLogging,
        Func<TMessageValue, Exception, Task>? OnException,
        Func<TMessageValue, Exception, ValueTask<string>>? OnInvalidExecutionContextToken);
    
    private IReadOnlyDictionary<string, Func<TMessageValue, Task>> BuildHandlersMap(
        CancellationToken cancellationToken)
    {
        return messageHandlerRegistrations
            .ToDictionary(
                pair => pair.Key,
                pair =>
                {
                    var registration = pair.Value;
                    var definitionSetup = new DefinitionSetup(
                        registration.OnMessage,
                        topicName,
                        withRetry,
                        retrySettings,
                        withDebugLogging,
                        onEventException,
                        executionContextTokenFallback);
                    var messageDefinition = CreateDefinition(definitionSetup);

                    registration.DefinitionCustomizer?.Invoke(messageDefinition);

                    return BuildHandler(messageDefinition, cancellationToken);
                });
    }

    private Func<TMessageValue, Task> WrapMessage<TMessage>(Func<TMessage, Task> onMessage)
        where TMessage : TMessageData
    {
        return message => onMessage(ConvertTo<TMessage>(message));
    }

    private Func<TMessageValue, Task> WrapMessage<TMessage>(Func<TMessage, KafkaMessageValueMetadata, Task> onMessage)
        where TMessage : TMessageData
    {
        return message => onMessage(ConvertTo<TMessage>(message), message.Metadata!);
    }

    private protected abstract TMessage ConvertTo<TMessage>(TMessageValue message) where TMessage : TMessageData;
    private protected abstract TBaseReaderBuilder Self { get; }

    private protected abstract string GetMessageTypeName<TConcreteMessage>() where TConcreteMessage : TMessageData;
    private protected abstract string GetMessageEntityType(TMessageValue messageValue);
    private protected abstract string GetMessageType(TMessageValue messageValue);
    private protected abstract TDefinition CreateDefinition(DefinitionSetup definitionSetup);
    private protected abstract Func<TMessageValue, Task> BuildHandler(TDefinition messageDefinition, CancellationToken cancellationToken);
}
