using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Docs.Kafka.Abstractions.Sales.Bills;
using Moedelo.Docs.Kafka.Abstractions.Sales.Bills.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.Bills
{
    [InjectAsSingleton(typeof(ISalesBillEventReader))]
    public sealed class SalesBillEventReader : DocsCudEventReaderBase<SalesBillCreatedMessage, SalesBillUpdatedMessage, SalesBillDeletedMessage>, ISalesBillEventReader
    {
        public SalesBillEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesBillEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        protected override string TopicName => AccountingPrimaryDocumentsTopics.Sales.Bills.CUD;
    }
}