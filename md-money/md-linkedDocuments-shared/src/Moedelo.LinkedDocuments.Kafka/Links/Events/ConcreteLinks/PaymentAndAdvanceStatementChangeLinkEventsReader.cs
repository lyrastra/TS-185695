using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks;

namespace Moedelo.LinkedDocuments.Kafka.Links.Events.ConcreteLinks
{
    [InjectAsSingleton(typeof(IPaymentAndAdvanceStatementChangeLinkEventsReader))]
    internal sealed class PaymentAndAdvanceStatementChangeLinkEventsReader : MoedeloKafkaTopicReaderBase, IPaymentAndAdvanceStatementChangeLinkEventsReader
    {
        public PaymentAndAdvanceStatementChangeLinkEventsReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PaymentAndAdvanceStatementChangeLinkEventsReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<PaymentToAdvanceStatementChangeLinkMessage, Task> onChange,
            Func<PaymentToAdvanceStatementChangeLinkMessage, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));
            var settings = new ReadTopicSetting<PaymentToAdvanceStatementChangeLinkMessage>(
                groupId, 
                LinksTopics.Event.PaymentAndAdvanceStatementChangeLinks, 
                onChange, 
                onException, 
                onFatalException:null,
                resetType: resetType, 
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}