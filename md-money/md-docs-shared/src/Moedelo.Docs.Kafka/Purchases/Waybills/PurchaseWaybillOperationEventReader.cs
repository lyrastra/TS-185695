using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Docs.Kafka.Abstractions.Purchases.Waybills;
using Moedelo.Docs.Shared.Kafka.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Purchases.Waybills
{
    [InjectAsSingleton(typeof(IPurchaseWaybillEventReaderBuilder))]
    public class PurchaseWaybillEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPurchaseWaybillEventReaderBuilder
    {
        public PurchaseWaybillEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                DocsTopics.PurchaseWaybill.Event.Topic,
                DocsTopics.PurchaseWaybill.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }
    }
}