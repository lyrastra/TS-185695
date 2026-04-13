using Moedelo.AccountingStatements.Kafka.Abstractions.PaymentForDocuments.Events;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.PaymentForDocuments
{
    public interface IPaymentForDocumentAccountingStatementEventReader
    {
        Task ReadAsync(
            string groupId,
            Func<PaymentForDocumentCreatedMessage, KafkaMessageValueMetadata, Task> onCreateAsync,
            Func<PaymentForDocumentDeletedMessage, KafkaMessageValueMetadata, Task> onDeleteAsync,
            Func<CDEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            ConsumerActionRetrySettings retrySettings = null,
            CancellationToken? cancellationToken = null);
    }
}