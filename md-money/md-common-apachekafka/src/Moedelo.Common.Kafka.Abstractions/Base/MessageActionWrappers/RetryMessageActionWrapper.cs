using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moedelo.Common.Audit.Abstractions.Helpers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Kafka.Abstractions.Settings;

namespace Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers
{
    public sealed class RetryMessageActionWrapper<TMessage> : IMessageActionWrapper<TMessage>
        where TMessage : MoedeloKafkaMessageValueBase
    {
        private readonly string topicName;
        private readonly ConsumerActionRetrySettings actionRetrySettings;
        private readonly IAuditTracer auditTracer;
        private readonly ILogger logger;
        private readonly CancellationToken cancellationToken;

        public RetryMessageActionWrapper(
            string topicName,
            ConsumerActionRetrySettings actionRetrySettings,
            IAuditTracer auditTracer,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            this.topicName = topicName;
            this.actionRetrySettings = actionRetrySettings;
            this.auditTracer = auditTracer;
            this.logger = logger ?? NullLogger.Instance;
            this.cancellationToken = cancellationToken;
        }

        public Func<TMessage, Task> Wrap(Func<TMessage, Task> onMessage)
        {
            return async message =>
            {
                var retryCount = 0;

                do
                {
                    try
                    {
                        await onMessage(message).ConfigureAwait(false);
                        break;
                    }
                    catch (Exception exception) when (actionRetrySettings.ExceptionsTypes?.Contains(exception.GetType()) != false)
                    {
                        retryCount++;
                        var needToRetry = retryCount < actionRetrySettings.MaxRetryCount;

                        if (needToRetry == false)
                        {
                            logger.LogErrorInRetryHandler(topicName, message.Metadata, exception, retryCount, message);
                            throw;
                        }

                        var retryTimeSpan = actionRetrySettings.RetryTimeoutStrategy
                                            ?.Invoke(actionRetrySettings.RetryTimeout, retryCount)
                                        ?? actionRetrySettings.RetryTimeout;

                        logger.LogErrorButRetryScheduled(topicName, message.Metadata, exception, retryTimeSpan, retryCount, message);

                        await auditTracer
                            .BuildSpan(AuditSpanType.InternalCode, "RetryMessageHandleDelay")
                            .WithTag("Delay.Seconds", (int)retryTimeSpan.TotalSeconds)
                            .RunAsync(Task.Delay, retryTimeSpan, cancellationToken);
                    }

                } while (!cancellationToken.IsCancellationRequested);
            };
        }
    }
}