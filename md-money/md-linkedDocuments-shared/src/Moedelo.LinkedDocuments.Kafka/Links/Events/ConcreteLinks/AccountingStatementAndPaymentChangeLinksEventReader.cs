using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.Kafka.Links.Events.ConcreteLinks
{
    [InjectAsSingleton(typeof(IAccountingStatementAndPaymentChangeLinksEventReader))]
    internal sealed class AccountingStatementAndPaymentChangeLinksEventReader : MoedeloKafkaTopicReaderBase, IAccountingStatementAndPaymentChangeLinksEventReader
    {
        public AccountingStatementAndPaymentChangeLinksEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<AccountingStatementAndPaymentChangeLinksEventReader> logger
        )
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<AccountingStatementAndPaymentChangeLinkMessage, Task> onChange,
            Func<AccountingStatementAndPaymentChangeLinkMessage, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));
            var settings = new ReadTopicSetting<AccountingStatementAndPaymentChangeLinkMessage>(
                groupId, 
                LinksTopics.Event.AccountingStatementAndPaymentChangeLinks, 
                onChange, 
                onException,
                onFatalException: null,
                resetType: resetType, 
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}