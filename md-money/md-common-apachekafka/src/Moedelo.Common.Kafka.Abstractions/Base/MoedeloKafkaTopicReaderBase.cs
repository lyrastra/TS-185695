using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public abstract class MoedeloKafkaTopicReaderBase : MoedeloKafkaTopicReaderInternalBase
    {
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAuditTracer auditTracer;
        private readonly ILogger logger;
        private bool useDebugLogging = false;

        private static readonly ConsumerActionRetrySettings DefaultRetrySettings =
            new (5, TimeSpan.FromMinutes(1));

        protected MoedeloKafkaTopicReaderBase(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger logger = null)
            : base(
                dependencies.ConsumerStarter,
                dependencies.SettingRepository,
                dependencies.TopicNameResolver,
                dependencies.KafkaConsumerBalancer,
                logger)
        {
            this.contextInitializer = dependencies.ContextInitializer;
            this.contextAccessor = dependencies.ContextAccessor;
            this.auditTracer = dependencies.AuditTracer;
            this.logger = logger;
        }

        protected bool UseDebugLogging
        {
            get => useDebugLogging;
            set
            {
                Debug.Assert(!value || logger != null, "Чтобы включить отладочное логирование, надо передать не пустой ILogger в конструктор");
                useDebugLogging = value;
            }
        }

        protected Task ReadTopicAsync<T>(
            ReadTopicSetting<T> settings,
            CancellationToken cancellationToken) 
            where T : MoedeloKafkaMessageValueBase
        {
            _ = settings ?? throw new ArgumentNullException(nameof(settings));

            var onExceptionWrapper = new OnExceptionActionWrapper<T>(settings.OnMessageException ?? DefaultOnMessageException);
            var auditWrapper = new AuditMessageActionWrapper<T>(settings.TopicName, auditTracer);

            var onMessage = 
                settings.OnMessage
                    .WrapByIf(useDebugLogging && logger != null, () => new LogMessageActionWrapper<T>(settings.TopicName, logger))
                    .WrapBy(onExceptionWrapper)
                    .WrapBy(auditWrapper)
                    .WrapByIf(settings.UseContext, () => new ExecutionContextMessageActionWrapper<T>(
                        contextInitializer,
                        contextAccessor,
                        settings.IgnoreExecutionContextOutdating,
                        settings.OnInvalidExecutionContextToken));

            return ReadTopicImpl(settings, onMessage, cancellationToken);
        }

        protected Task ReadTopicWithRetryAsync<T>(
            ReadTopicSetting<T> settings,
            CancellationToken cancellationToken) where T : MoedeloKafkaMessageValueBase
        {
            return ReadTopicWithRetryAsync(settings, null, cancellationToken);
        }

        protected Task ReadTopicWithRetryAsync<T>(
            ReadTopicSetting<T> settings,
            ConsumerActionRetrySettings retrySettings,
            CancellationToken cancellationToken) where T : MoedeloKafkaMessageValueBase
        {
            _ = settings ?? throw new ArgumentException(nameof(settings));

            var retryWrapper = new RetryMessageActionWrapper<T>(settings.TopicName, retrySettings ?? DefaultRetrySettings, auditTracer, logger, cancellationToken);
            var onExceptionWrapper = new OnExceptionActionWrapper<T>(settings.OnMessageException ?? DefaultOnMessageException);
            var auditWrapper = new AuditMessageActionWrapper<T>(settings.TopicName, auditTracer);

            var onMessage =
                settings.OnMessage
                    .WrapBy(retryWrapper)
                    .WrapByIf(useDebugLogging && logger != null, () => new LogMessageActionWrapper<T>(settings.TopicName, logger))
                    .WrapBy(onExceptionWrapper)
                    .WrapBy(auditWrapper)
                    .WrapByIf(settings.UseContext, () => new ExecutionContextMessageActionWrapper<T>(
                        contextInitializer,
                        contextAccessor,
                        settings.IgnoreExecutionContextOutdating,
                        settings.OnInvalidExecutionContextToken));

            return ReadTopicImpl(settings, onMessage, cancellationToken);
        }

        private Task DefaultOnMessageException<T>(T message, Exception ex) where T : MoedeloKafkaMessageValueBase
        {
            if (logger == null)
            {
                return Task.CompletedTask;
            }

            var metadata = message?.Metadata;

            logger.LogError(ex,"An error occurred while processing {TypeName}. Metadata: {Metadata}",
                    typeof(T).Name, metadata?.ToJsonString());

            return Task.CompletedTask;
        }
    }
}