using Moedelo.AccountingStatements.Kafka.Abstractions.TradingFee.Events;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.TradingFee
{
    public interface ITradingFeeAccountingStatementEventReader
    {
        Task ReadAsync(
            string groupId,
            Func<TradingFeeCreatedMessage, KafkaMessageValueMetadata, Task> onCreateAsync,
            Func<TradingFeeDeletedMessage, KafkaMessageValueMetadata, Task> onDeleteAsync,
            Func<CDEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            ConsumerActionRetrySettings retrySettings = null,
            CancellationToken? cancellationToken = null);
    }
}