using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Docs.Kafka.Abstractions.Sales.Waybills;
using Moedelo.Docs.Shared.Kafka.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.Waybills
{
    [InjectAsSingleton(typeof(ISaleWaybillEventReaderBuilder))]
    public sealed class SaleWaybillEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ISaleWaybillEventReaderBuilder 
    {
        public SaleWaybillEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                DocsTopics.SaleWaybill.Event.Topic,
                DocsTopics.SaleWaybill.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }
    }
}