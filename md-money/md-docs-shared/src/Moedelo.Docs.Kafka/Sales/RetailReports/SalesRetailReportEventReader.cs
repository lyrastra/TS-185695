using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Docs.Kafka.Abstractions.Sales.RetailReports;
using Moedelo.Docs.Kafka.Abstractions.Sales.RetailReports.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.RetailReports
{
    [InjectAsSingleton(typeof(ISalesRetailReportEventReader))]
    public sealed class SalesRetailReportEventReader : DocsCudEventReaderBase<SalesRetailReportCreatedMessage, SalesRetailReportUpdatedMessage, SalesRetailReportDeletedMessage>, ISalesRetailReportEventReader
    {
        public SalesRetailReportEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesRetailReportEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        protected override string TopicName => AccountingPrimaryDocumentsTopics.Sales.RetailReports.CUD;
    }
}