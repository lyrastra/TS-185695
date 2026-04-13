using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands.PurseOperations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IPurseOperationChangeTaxationSystemCommandReader))]
    internal sealed class PurseOperationChangeTaxationSystemCommandReader : MoedeloKafkaTopicReaderBase, IPurseOperationChangeTaxationSystemCommandReader
    {
        public PurseOperationChangeTaxationSystemCommandReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PurseOperationChangeTaxationSystemCommandReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<PurseOperationChangeTaxationSystemCommandMessage, KafkaMessageValueMetadata, Task> onChange,
            Func<PurseOperationChangeTaxationSystemCommandMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));

            Task OnMessageActionAsync(PurseOperationChangeTaxationSystemCommandMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                return onChange(Map(messageValue), metadata);
            }

            var settings = new ReadTopicSetting<PurseOperationChangeTaxationSystemCommandMessageValue>(
                groupId, MoneyTopics.Commands.PurseOperationChangeTaxationSystemCommand, OnMessageActionAsync, onFatalException, null, resetType, consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }

        private static PurseOperationChangeTaxationSystemCommandMessage Map(PurseOperationChangeTaxationSystemCommandMessageValue messageValue)
        {
            return new PurseOperationChangeTaxationSystemCommandMessage
            {
                DocumentBaseId = messageValue.DocumentBaseId,
                TaxationSystemType = messageValue.TaxationSystemType,
                Guid = messageValue.Guid
            };
        }
    }
}