using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.CashOrders;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(ICashOrderChangeTaxationSystemCommandReader))]
    internal sealed class CashOrderChangeTaxationSystemCommandReader : MoedeloKafkaTopicReaderBase, ICashOrderChangeTaxationSystemCommandReader
    {
        public CashOrderChangeTaxationSystemCommandReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<CashOrderChangeTaxationSystemCommandReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<CashOrderChangeTaxationSystemCommandMessage, KafkaMessageValueMetadata, Task> onChange,
            Func<CashOrderChangeTaxationSystemCommandMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));

            Task OnMessageActionAsync(CashOrderChangeTaxationSystemCommandMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                return onChange(Map(messageValue), metadata);
            }

            var settings = new ReadTopicSetting<CashOrderChangeTaxationSystemCommandMessageValue>(
                groupId, MoneyTopics.Commands.CashOrderChangeTaxationSystemCommand, OnMessageActionAsync, onFatalException, null, resetType, consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }

        private static CashOrderChangeTaxationSystemCommandMessage Map(CashOrderChangeTaxationSystemCommandMessageValue messageValue)
        {
            return new CashOrderChangeTaxationSystemCommandMessage
            {
                DocumentBaseId = messageValue.DocumentBaseId,
                TaxationSystemType = messageValue.TaxationSystemType,
                Guid = messageValue.Guid
            };
        }
    }
}