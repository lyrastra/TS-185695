using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills;
using Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills.Events;
using Moedelo.Stock.Kafka.Abstractions.Topics;

namespace Moedelo.Stock.Kafka.RequisitionWaybills
{
    [InjectAsSingleton(typeof(IRequisitionWaybillEventReaderBuilder))]
    public class RequisitionWaybillEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRequisitionWaybillEventReaderBuilder
    {
        public RequisitionWaybillEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                StockTopics.RequisitionWaybill.Event.Topic,
                StockTopics.RequisitionWaybill.Entity,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IRequisitionWaybillEventReaderBuilder OnCreated(Func<RequisitionWaybillCreatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRequisitionWaybillEventReaderBuilder OnUpdated(Func<RequisitionWaybillUpdatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRequisitionWaybillEventReaderBuilder OnDeleted(Func<RequisitionWaybillDeletedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}