using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Docs.Kafka.Abstractions.Purchases.Upds;
using Moedelo.Docs.Shared.Kafka.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Purchases.Upds
{
    [InjectAsSingleton(typeof(IPurchaseUpdEventReaderBuilder))]
    public class PurchaseUpdEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPurchaseUpdEventReaderBuilder
    {
        public PurchaseUpdEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                DocsTopics.PurchaseUpd.Event.Topic,
                DocsTopics.PurchaseUpd.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }
    }
}