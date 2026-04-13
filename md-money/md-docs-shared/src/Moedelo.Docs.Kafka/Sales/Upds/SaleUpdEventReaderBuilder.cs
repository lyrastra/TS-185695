using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Docs.Kafka.Abstractions.Sales.Upds;
using Moedelo.Docs.Shared.Kafka.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.Upds
{
    [InjectAsSingleton(typeof(ISaleUpdEventReaderBuilder))]
    public sealed class SaleUpdEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ISaleUpdEventReaderBuilder 
    {
        public SaleUpdEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                DocsTopics.SaleUpd.Event.Topic,
                DocsTopics.SaleUpd.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }
    }
}