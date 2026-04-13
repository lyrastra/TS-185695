using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountEventReader))]
    internal sealed class CurrencyTransferToAccountEventReader : MoedeloKafkaTopicReaderBase, ICurrencyTransferToAccountEventReader
    {
        public CurrencyTransferToAccountEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<CurrencyTransferToAccountEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<CurrencyTransferToAccountCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<CurrencyTransferToAccountUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<CurrencyTransferToAccountDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<CUDEventMessageValue, Exception, Task> onException = null,
            Func<Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType =
                KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onCreate, nameof(onCreate));
            ValidateHandlerFunc(onUpdate, nameof(onUpdate));
            ValidateHandlerFunc(onDelete, nameof(onDelete));

            Task OnMessageAction(CUDEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;

                switch (messageValue.EventType)
                {
                    case CUDEventType.Created:
                        var createdMessage = messageValue.EventDataJson.FromJsonString<CurrencyTransferToAccountCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case CUDEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<CurrencyTransferToAccountUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case CUDEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<CurrencyTransferToAccountDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }

            var topicName = PaymentOrderTopics.CurrencyTransferToAccount.Event.CUD;
            var settings = new ReadTopicSetting<CUDEventMessageValue>(
                groupId,
                topicName,
                OnMessageAction,
                onException,
                onFatalException,
                resetType,
                consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}
