using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Business.Abstractions;
using Moedelo.Money.Business.Abstractions.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Events
{
    [InjectAsSingleton(typeof(ITaxationSystemChangedEventReader))]
    internal sealed class TaxationSystemChangedEventReader : MoedeloKafkaTopicReaderBase, ITaxationSystemChangedEventReader
    {
        public TaxationSystemChangedEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<TaxationSystemChangedEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<TaxationSystemChangedEvent, KafkaMessageValueMetadata, Task> onChange,
            Func<TaxationSystemChangedEventMessageValue, Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));

            Task OnMessageActionAsync(TaxationSystemChangedEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                return onChange(Map(messageValue), metadata);
            }

            var settings = new ReadTopicSetting<TaxationSystemChangedEventMessageValue>(
                groupId, MoneyTopics.Events.TaxationSystemChangedEvent, OnMessageActionAsync, onFatalException, null, resetType, consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }

        private static TaxationSystemChangedEvent Map(TaxationSystemChangedEventMessageValue messageValue)
        {
            return new TaxationSystemChangedEvent
            {
                DocumentBaseId = messageValue.DocumentBaseId,
                TaxationSystemType = messageValue.TaxationSystemType,
                Guid = messageValue.Guid
            };
        }
    }
}