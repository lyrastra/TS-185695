using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Commands
{
    [InjectAsSingleton(typeof(IChangeTaxationSystemCommandReader))]
    internal sealed class ChangeTaxationSystemCommandReader : MoedeloKafkaTopicReaderBase, IChangeTaxationSystemCommandReader
    {
        public ChangeTaxationSystemCommandReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<ChangeTaxationSystemCommandReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<ChangeTaxationSystemCommand, KafkaMessageValueMetadata, Task> onChange,
            Func<ChangeTaxationSystemCommandMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));

            Task OnMessageActionAsync(ChangeTaxationSystemCommandMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                return onChange(Map(messageValue), metadata);
            }

            var settings = new ReadTopicSetting<ChangeTaxationSystemCommandMessageValue>(
                groupId, MoneyTopics.Commands.ChangeTaxationSystemCommand, OnMessageActionAsync, onFatalException, null, resetType, consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }

        private static ChangeTaxationSystemCommand Map(ChangeTaxationSystemCommandMessageValue messageValue)
        {
            return new ChangeTaxationSystemCommand
            {
                DocumentBaseIds = messageValue.DocumentBaseIds,
                TaxationSystemType = messageValue.TaxationSystemType,
                Guid = messageValue.Guid
            };
        }
    }
}