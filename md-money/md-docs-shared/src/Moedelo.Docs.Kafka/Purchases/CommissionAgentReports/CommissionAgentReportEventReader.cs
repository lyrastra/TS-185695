using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Messages;
using Moedelo.Docs.Kafka.Abstractions.Purchases.CommissionAgentReports.Readers;
using Moedelo.Docs.Kafka.Abstractions.Topics.ByApps;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Purchases.CommissionAgentReports
{
    [InjectAsSingleton(typeof(ICommissionAgentReportEventReader))]
    internal sealed class CommissionAgentReportEventReader : DocsCudEventReaderBase<CommissionAgentReportCreatedMessage, CommissionAgentReportUpdatedMessage, CommissionAgentReportDeletedMessage>, ICommissionAgentReportEventReader
    {
        public CommissionAgentReportEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<CommissionAgentReportEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        protected override string TopicName => CommissionAgentReportTopics.Purchases.Event.CUD;
    }
}