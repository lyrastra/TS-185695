using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.RabbitMQ.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Interfaces;
using Moedelo.Infrastructure.RabbitMQ.Abstractions.Models;

namespace Moedelo.Common.RabbitMQ
{
    [InjectAsSingleton(typeof(IMoedeloRabbitMqReader))]
    internal sealed class MoedeloRabbitMqReader : IMoedeloRabbitMqReader
    {
        private readonly IMoedeloRabbitMqConfigurator moedeloRabbitMqConfigurator;
        private readonly IRabbitMqConsumerFactory rabbitMqConsumerFactory;
        private readonly IRabbitMqProducer rabbitMqProducer;
        private readonly IAuditTracer auditTracer;

        public MoedeloRabbitMqReader(
            IMoedeloRabbitMqConfigurator moedeloRabbitMqConfigurator,
            IRabbitMqConsumerFactory rabbitMqConsumerFactory,
            IRabbitMqProducer rabbitMqProducer,
            IAuditTracer auditTracer)
        {
            this.moedeloRabbitMqConfigurator = moedeloRabbitMqConfigurator;
            this.auditTracer = auditTracer;
            this.rabbitMqConsumerFactory = rabbitMqConsumerFactory;
            this.rabbitMqProducer = rabbitMqProducer;
        }

        public Task ReadWithRetryAsync<T>(
            string groupId,
            string exchangeName,
            Func<T, Task> onMessage,
            Func<T, Exception, Task> onException = null,
            ReadWithRetryOptions options = null)
        {
            return ReadWithRetryAsync(
                groupId,
                exchangeName,
                (messageData, _) => onMessage(messageData),
                onException,
                options);
        }

        public Task ReadWithRetryAsync<T>(
            string groupId,
            string exchangeName,
            Func<T, uint, Task> onMessage,
            Func<T, Exception, Task> onException = null,
            ReadWithRetryOptions options = null)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new ArgumentNullException(nameof(groupId));
            }

            if (string.IsNullOrEmpty(exchangeName))
            {
                throw new ArgumentNullException(nameof(exchangeName));
            }

            var connectionString = moedeloRabbitMqConfigurator.GetConnection();
            var fullExchangeName = $"{moedeloRabbitMqConfigurator.GetExchangeNamePrefix()}.MD.{exchangeName}.V2.E";
            var queueName = $"{moedeloRabbitMqConfigurator.GetExchangeNamePrefix()}.MD.{exchangeName}.V2.Q.{groupId}";

            if (options == null)
            {
                options = new ReadWithRetryOptions();
            }

            var currentClientRoutingKey = $"ClientId.{groupId}";

            var exchangeConsumerConnection = new ConsumerQueueExchangeConnection(
                connectionString,
                fullExchangeName,
                queueName,
                "ClientId.All", currentClientRoutingKey);
            
            async Task OnExceptionWrapper(MessageWrapper<T> messageWrapper, Exception ex)
            {
                if (onException == null)
                {
                    return;
                }

                try
                {
                    await onException(messageWrapper.Data, ex).ConfigureAwait(false);
                }
                catch
                {
                    //ignore
                }
            }

            async Task OnMessageWrapper(MessageWrapper<T> messageWrapper)
            {
                var parentSpanContext = messageWrapper.AuditSpanContext;

                using (var scope = auditTracer
                    .BuildSpan(AuditSpanType.EventApiHandler, $"Handle {typeof(T).Name} from {queueName}")
                    .AsChildOf(parentSpanContext)
                    .IgnoreTraceId()
                    .WithTag("Message", messageWrapper)
                    .Start())
                {
                    try
                    {
                        await onMessage(messageWrapper.Data, messageWrapper.RepeatCount).ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        scope.Span.SetError(e);

                        await OnExceptionWrapper(messageWrapper, e).ConfigureAwait(false);
                        
                        uint repeatCount = messageWrapper.RepeatCount + 1;

                        if (repeatCount >= options.MaxRetryCount)
                        {
                            return;
                        }

                        var spanContext = scope.Span.Context;
                        var maxDelay = TimeSpan.FromDays(1);
                        var delay = options.DelayFunc(repeatCount);
                        var messageDelay = maxDelay < delay ? maxDelay : delay;
                        
                        await rabbitMqProducer.ProduceAsync(
                                new ProducerExchangeConnection(connectionString, fullExchangeName),
                                currentClientRoutingKey, new MessageWrapper<T>
                                {
                                    Data = messageWrapper.Data,
                                    RepeatCount = repeatCount,
                                    Delay = messageDelay,
                                    AuditSpanContext = spanContext.IsNullOrEmpty()
                                        ? null
                                        : new AuditSpanContext
                                        {
                                            AsyncTraceId = spanContext.AsyncTraceId,
                                            TraceId = spanContext.TraceId,
                                            ParentId = spanContext.ParentId,
                                            CurrentId = spanContext.CurrentId,
                                            CurrentDepth = spanContext.CurrentDepth
                                        }
                                }, (int)messageDelay.TotalMilliseconds)
                            .ConfigureAwait(false);
                    }
                }
            }

            return rabbitMqConsumerFactory.ListenAsync<MessageWrapper<T>>(
                exchangeConsumerConnection,
                OnMessageWrapper,
                OnExceptionWrapper,
                new ConsumerOptionalConfiguration(options.PrefetchCount));
        }
    }
}