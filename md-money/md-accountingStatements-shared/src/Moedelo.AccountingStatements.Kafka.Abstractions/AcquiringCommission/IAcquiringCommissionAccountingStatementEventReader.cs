using Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission.Events;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.AcquiringCommission
{
    public interface IAcquiringCommissionAccountingStatementEventReader
    {
        Task ReadAsync(
            string groupId,
            Func<AcquiringCommissionCreatedMessage, KafkaMessageValueMetadata, Task> onCreateAsync,
            Func<AcquiringCommissionDeletedMessage, KafkaMessageValueMetadata, Task> onDeleteAsync,
            Func<CDEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            ConsumerActionRetrySettings retrySettings = null,
            CancellationToken? cancellationToken = null);
    }
}