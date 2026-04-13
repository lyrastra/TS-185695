using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Models.EventBus;
using Moedelo.InfrastructureV2.EventBus.Extensions;
using Moedelo.InfrastructureV2.EventBus.Internal;
using Moedelo.InfrastructureV2.EventBus.Internal.Pools;

namespace Moedelo.InfrastructureV2.EventBus;

[InjectAsSingleton(typeof(IConsumerFactory))]
internal sealed class ConsumerFactory : IConsumerFactory
{
    private const string Tag = nameof(ConsumerFactory);

    private readonly ILogger logger;
    private readonly IEventBusConfigurator eventBusConfigurator;
    private readonly ITopicExchangeConsumerFactory topicExchangeConsumerFactory;
    private readonly ITopicExchangePublisherPool topicExchangePublisherPool;
    private readonly IAuditTracer auditTracer;
    private readonly IAuditScopeManager auditScopeManager;

    public ConsumerFactory(
        ILogger logger,
        IEventBusConfigurator eventBusConfigurator,
        ITopicExchangeConsumerFactory topicExchangeConsumerFactory,
        ITopicExchangePublisherPool topicExchangePublisherPool,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
    {
        this.logger = logger;
        this.eventBusConfigurator = eventBusConfigurator;
        this.topicExchangeConsumerFactory = topicExchangeConsumerFactory;
        this.topicExchangePublisherPool = topicExchangePublisherPool;
        this.auditTracer = auditTracer;
        this.auditScopeManager = auditScopeManager;
    }

    public Task StartConsumerWithRetryAsync<T>(EventBusEventDefinition<T> definition, Func<T, Task> onMessage,
        ConsumerWithRetryOptions options = null)
        where T : class
    {
        return StartConsumerWithRetryAsync(definition, (messageData, _) => onMessage(messageData), options);
    }

    public Task StartConsumerWithRetryAsync<TMessage>(
        EventBusEventDefinition<TMessage> definition,
        IConsumerFactory.MessageHandler<TMessage> onMessage,
        ConsumerWithRetryOptions options = null) where TMessage : class
    {
        const string broadcastRoutingKey = "ClientId.All"; 
        var clientRoutingKey = eventBusConfigurator.GetClientRoutingKey();

        var connectionString = eventBusConfigurator.GetEventBusConfig();
        var exchangeName = eventBusConfigurator.GetExchangeName(definition);
        var queueName = eventBusConfigurator.GetQueueName(definition);

        options ??= new ConsumerWithRetryOptions();

        var exchangeConsumerConnection = new ExchangeConsumerConnection(
            connectionString,
            exchangeName,
            queueName,
            options.PrefetchCount,
            options.SkipRedelivered,
            broadcastRoutingKey, clientRoutingKey);

        var directClientPublisher = new DirectClientPublisher<TMessage>(
            connectionString,
            exchangeName,
            clientRoutingKey,
            topicExchangePublisherPool,
            auditScopeManager);

        return topicExchangeConsumerFactory
            .ListenAsync(exchangeConsumerConnection, (Func<MessageWrapper<TMessage>, Task>)OnMessageWrapper);

        async Task OnMessageWrapper(MessageWrapper<TMessage> messageWrapper)
        {
            var parentSpanContext = messageWrapper.AuditSpanContext;

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.EventApiHandler, $"Handle {typeof(TMessage).Name} from {queueName}")
                .AsChildOf(parentSpanContext)
                .IgnoreTraceId()
                .WithTag("Message", messageWrapper)
                .Start();

            try
            {
                await onMessage(messageWrapper.Data, messageWrapper.RepeatCount).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                var repeatCount = messageWrapper.RepeatCount + 1;
                var isRetryLast = repeatCount >= options.MaxRetryCount;

                if (isRetryLast || !options.LogErrorOnlyOnLastRetryFailure)
                {
                    logger.Error(Tag, $"Ошибка обработки сообщения {typeof(TMessage).Name}", exception,
                        extraData: new
                        {
                            MessageType = typeof(TMessage).Name,
                            Message = messageWrapper
                        });
                }

                if (isRetryLast)
                {
                    return;
                }

                await directClientPublisher
                    .PublishAsync(messageWrapper.Data, repeatCount, options.DelayFunc(repeatCount))
                    .ConfigureAwait(false);
            }
        }
    }
}