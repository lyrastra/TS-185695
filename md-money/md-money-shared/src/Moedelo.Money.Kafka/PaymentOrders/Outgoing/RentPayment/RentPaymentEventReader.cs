using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentEventReader))]
    internal sealed class RentPaymentEventReader : MoedeloKafkaTopicReaderBase, IRentPaymentEventReader
    {
        public RentPaymentEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<RentPaymentEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<RentPaymentCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<RentPaymentUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<RentPaymentDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
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
                        var createdMessage = messageValue.EventDataJson.FromJsonString<RentPaymentCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case CUDEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<RentPaymentUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case CUDEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<RentPaymentDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }

            var topicName = PaymentOrderTopics.RentPayment.Event.CUD;
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
