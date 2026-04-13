using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Commands.PaymentOrders.MissingEmployee;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IPaymentOrderSetMissingEmployeeCommandReader))]
    internal sealed class PaymentOrderSetMissingEmployeeCommandReader : MoedeloKafkaTopicReaderBase, IPaymentOrderSetMissingEmployeeCommandReader
    {
        public PaymentOrderSetMissingEmployeeCommandReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PaymentOrderSetMissingEmployeeCommandReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<PaymentOrderSetMissingEmployeeCommandMessage, KafkaMessageValueMetadata, Task> onChange,
            Func<PaymentOrderSetMissingEmployeeCommandMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));

            Task OnMessageActionAsync(PaymentOrderSetMissingEmployeeCommandMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                return onChange(Map(messageValue), metadata);
            }

            var settings = new ReadTopicSetting<PaymentOrderSetMissingEmployeeCommandMessageValue>(
                groupId, MoneyTopics.Commands.PaymentOrderSetMissingEmployeeCommand, OnMessageActionAsync, onFatalException, null, resetType, consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }

        private static PaymentOrderSetMissingEmployeeCommandMessage Map(PaymentOrderSetMissingEmployeeCommandMessageValue messageValue)
        {
            return new PaymentOrderSetMissingEmployeeCommandMessage
            {
                EmployeeId = messageValue.EmployeeId,
                DocumentBaseIds = messageValue.DocumentBaseIds
            };
        }
    }
}