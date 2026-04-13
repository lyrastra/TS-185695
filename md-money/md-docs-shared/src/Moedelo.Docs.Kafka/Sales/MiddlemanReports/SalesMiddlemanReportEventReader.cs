using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Docs.Kafka.Abstractions.Sales.MiddlemanReports;
using Moedelo.Docs.Kafka.Abstractions.Sales.MiddlemanReports.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.MiddlemanReports
{
    [InjectAsSingleton(typeof(ISalesMiddlemanReportEventReader))]
    public sealed class SalesMiddlemanReportEventReader : DocsCudEventReaderBase<SalesMiddlemanReportCreatedMessage, SalesMiddlemanReportUpdatedMessage, SalesMiddlemanReportDeletedMessage>, ISalesMiddlemanReportEventReader
    {
        public SalesMiddlemanReportEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesMiddlemanReportEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        protected override string TopicName => AccountingPrimaryDocumentsTopics.Sales.MiddlemanReports.CUD;
    }
}