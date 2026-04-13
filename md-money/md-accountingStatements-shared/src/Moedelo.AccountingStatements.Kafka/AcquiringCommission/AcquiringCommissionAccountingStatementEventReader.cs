using Microsoft.Extensions.Logging;
using Moedelo.AccountingStatements.Kafka.Abstractions;
using Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission;
using Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Events;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingStatements.Kafka.AcquiringCommission
{
    [InjectAsSingleton(typeof(IAcquiringCommissionAccountingStatementEventReader))]
    internal sealed class AcquiringCommissionAccountingStatementEventReader : MoedeloKafkaTopicReaderBase, IAcquiringCommissionAccountingStatementEventReader
    {
        public AcquiringCommissionAccountingStatementEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<AcquiringCommissionAccountingStatementEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<AcquiringCommissionCreatedMessage, KafkaMessageValueMetadata, Task> onCreateAsync,
            Func<AcquiringCommissionDeletedMessage, KafkaMessageValueMetadata, Task> onDeleteAsync,
            Func<CDEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            ConsumerActionRetrySettings retrySettings = null,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onCreateAsync, nameof(onCreateAsync));
            ValidateHandlerFunc(onDeleteAsync, nameof(onDeleteAsync));

            Task OnMessageAction(CDEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                switch (messageValue.EventType)
                {
                    case AccountingStatementEventType.Created:
                        var createdMessage = messageValue.EventDataJson.FromJsonString<AcquiringCommissionCreatedMessage>();
                        return onCreateAsync(createdMessage, metadata);
                    case AccountingStatementEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<AcquiringCommissionDeletedMessage>();
                        return onDeleteAsync(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }

            var settings = new ReadTopicSetting<CDEventMessageValue>(
                groupId,
                AccountingStatementsTopics.Event.AcquiringCommissionCD,
                OnMessageAction,
                onException,
                null,
                resetType: resetType,
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, retrySettings, cancellationToken ?? CancellationToken.None);
        }
    }
}
