using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Docs.Kafka.Abstractions.Sales.Statements;
using Moedelo.Docs.Kafka.Abstractions.Sales.Statements.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.Statements
{
    [InjectAsSingleton(typeof(ISalesStatementEventReader))]
    public sealed class SalesStatementEventReader : DocsCudEventReaderBase<SalesStatementCreatedMessage, SalesStatementUpdatedMessage, SalesStatementDeletedMessage>, ISalesStatementEventReader
    {
        public SalesStatementEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesStatementEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        protected override string TopicName => AccountingPrimaryDocumentsTopics.Sales.Statements.CUD;
    }
}
