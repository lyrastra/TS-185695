using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Docs.Kafka.Abstractions.Purchases.Statements;
using Moedelo.Docs.Kafka.Abstractions.Purchases.Statements.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Purchases.Statements
{
    [InjectAsSingleton(typeof(IPurchasesStatementEventReader))]
    public sealed class PurchasesStatementEventReader : DocsCudEventReaderBase<PurchasesStatementCreatedMessage, PurchasesStatementUpdatedMessage, PurchasesStatementDeletedMessage>, IPurchasesStatementEventReader
    {
        public PurchasesStatementEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PurchasesStatementEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        protected override string TopicName => AccountingPrimaryDocumentsTopics.Purchases.Statements.CUD;
    }
}