using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Docs.Kafka.Abstractions;
using Moedelo.Docs.Kafka.Abstractions.Sales.Bills;
using Moedelo.Docs.Kafka.Abstractions.Sales.Bills.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka.Sales.Bills
{
    [InjectAsSingleton(typeof(ISalesBillPaymentUpdatedReader))]
    public sealed class SalesBillPaymentUpdatedReader : MoedeloKafkaTopicReaderBase, ISalesBillPaymentUpdatedReader
    {
        private readonly ILogger _logger;
        private string _topicName = AccountingPrimaryDocumentsTopics.Sales.Bills.PaymentChanged;

        public SalesBillPaymentUpdatedReader(IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesBillPaymentUpdatedReader> logger) 
            : base(dependencies, logger)
        {
            _logger = logger;
        }

        public async Task ReadAsync(string groupId,
            Func<SalesBillPaymentMessage, KafkaMessageValueMetadata, Task> onPaymentUpdate,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            _logger.LogInformation("Begin Sales Bill payment update read message from kafka");

            Task OnMessageAction(DocsCudEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                var createdMessage = messageValue.EventDataJson.FromJsonString<SalesBillPaymentMessage>();
                return onPaymentUpdate(createdMessage, metadata);
            }

            var settings = new ReadTopicSetting<DocsCudEventMessageValue>
            (groupId,
                _topicName,
                OnMessageAction,
                onException,
                onFatalException: null,
                resetType: resetType,
                consumersCount: consumerCount);
            try
            {
                await ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error Sales Bill payment update read message from kafka {e.Message}");
            }
        }
    }
}