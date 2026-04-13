#nullable enable
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Messages;

internal abstract class MoedeloEntityMessageHandlerDefinition<TDefinition, TMessageValue>
    : IMoedeloEntityMessageHandlerDefinition<TDefinition, TMessageValue>
    where TMessageValue: MoedeloKafkaMessageValueBase
{
    private readonly ConsumerActionRetrySettings defaultConsumerActionRetrySettings
        = new (maxRetryCount: 5, TimeSpan.FromMinutes(1));

    private readonly IExecutionInfoContextInitializer contextInitializer;
    private readonly IExecutionInfoContextAccessor contextAccessor;
    private readonly IAuditTracer auditTracer;
    private readonly ILogger? logger;

    private readonly string topicName;
    private bool withRetry;
    private bool useDebugLogging;
    private bool withoutExecutionContext;
    private readonly Func<TMessageValue, Task> onMessage;
    private Func<TMessageValue, Exception, ValueTask<string>>? executionContextTokenFallback;
    private Func<TMessageValue, Exception, Task>? onExceptionHandler;
    private ConsumerActionRetrySettings? retrySettings;
    private bool ignoreExecutionContextOutdating;
    

    internal MoedeloEntityMessageHandlerDefinition(
        string topicName,
        IExecutionInfoContextInitializer contextInitializer,
        IExecutionInfoContextAccessor contextAccessor,
        IAuditTracer auditTracer,
        ILogger? logger,
        Func<TMessageValue, Task> onEvent)
    {
        this.topicName = topicName.EnsureIsNotNullOrWhiteSpace(nameof(topicName));
        this.contextInitializer = contextInitializer;
        this.contextAccessor = contextAccessor;
        this.auditTracer = auditTracer;
        this.logger = logger;
        this.onMessage = onEvent.EnsureIsNotNull(nameof(onEvent));
    }

    private protected abstract TDefinition Self { get; }

    public TDefinition SetWithRetry(
        // ReSharper disable once ParameterHidesMember
        bool withRetry,
        ConsumerActionRetrySettings? settings)
    {
        this.withRetry = withRetry;
        retrySettings = withRetry ? (settings ?? defaultConsumerActionRetrySettings) : null;

        return Self;
    }

    public TDefinition SetDebugLogging(bool value)
    {
        Debug.Assert(!value || logger != null, "Чтобы включить отладочное логирование, надо передать не пустой ILogger в конструктор");
        useDebugLogging = value;

        return Self;
    }

    public TDefinition SetOnMessageExceptionHandler(
        Func<TMessageValue, Exception, Task>? onException)
    {
        onExceptionHandler = onException;

        return Self;
    }

    public TDefinition IgnoreExecutionContextOutdating(bool value)
    {
        ignoreExecutionContextOutdating = value;

        return Self;
    }

    public TDefinition WithoutContext()
    {
        withoutExecutionContext = true;

        return Self;
    }

    public TDefinition SetExecutionContextTokenFallback(
        Func<TMessageValue, Exception, ValueTask<string>>? onTokenDeserializationFailed)
    {
        this.executionContextTokenFallback = onTokenDeserializationFailed;

        return Self;
    }

    internal Func<TMessageValue, Task> Build(CancellationToken cancellationToken)
    {
        var auditWrapper = new AuditMessageActionWrapper<TMessageValue>(topicName, auditTracer);

        return this.onMessage
            .WrapByIf(withRetry, () => new RetryMessageActionWrapper<TMessageValue>(topicName, retrySettings, auditTracer, logger, cancellationToken))
            .WrapByIf(useDebugLogging && logger != null, () => new LogMessageActionWrapper<TMessageValue>(topicName, logger))
            .WrapByIf(onExceptionHandler != null, () => new OnExceptionActionWrapper<TMessageValue>(onExceptionHandler))
            .WrapBy(auditWrapper)
            .WrapByIf(withoutExecutionContext == false, () => new ExecutionContextMessageActionWrapper<TMessageValue>(
                contextInitializer,
                contextAccessor,
                ignoreExecutionContextOutdating,
                executionContextTokenFallback));
    }
}
