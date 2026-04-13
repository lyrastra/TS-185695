using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IPaymentOrderChangeTaxationSystemCommandReader))]
    internal sealed class PaymentOrderChangeTaxationSystemCommandReader : MoedeloKafkaTopicReaderBase, IPaymentOrderChangeTaxationSystemCommandReader
    {
        public PaymentOrderChangeTaxationSystemCommandReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PaymentOrderChangeTaxationSystemCommandReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<PaymentOrderChangeTaxationSystemCommandMessage, KafkaMessageValueMetadata, Task> onChange,
            Func<PaymentOrderChangeTaxationSystemCommandMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));

            Task OnMessageActionAsync(PaymentOrderChangeTaxationSystemCommandMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                return onChange(Map(messageValue), metadata);
            }

            var settings = new ReadTopicSetting<PaymentOrderChangeTaxationSystemCommandMessageValue>(
                groupId, MoneyTopics.Commands.PaymentOrderChangeTaxationSystemCommand, OnMessageActionAsync, onFatalException, null, resetType, consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }

        private static PaymentOrderChangeTaxationSystemCommandMessage Map(PaymentOrderChangeTaxationSystemCommandMessageValue messageValue)
        {
            return new PaymentOrderChangeTaxationSystemCommandMessage
            {
                DocumentBaseId = messageValue.DocumentBaseId,
                TaxationSystemType = messageValue.TaxationSystemType,
                Guid = messageValue.Guid
            };
        }
    }
}