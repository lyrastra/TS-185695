using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills.Events;

namespace Moedelo.Stock.Kafka.Abstractions.RequisitionWaybills
{
    public interface IRequisitionWaybillEventReaderBuilder: IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRequisitionWaybillEventReaderBuilder OnCreated(Func<RequisitionWaybillCreatedMessage, Task> onEvent);

        IRequisitionWaybillEventReaderBuilder OnUpdated(Func<RequisitionWaybillUpdatedMessage, Task> onEvent);

        IRequisitionWaybillEventReaderBuilder OnDeleted(Func<RequisitionWaybillDeletedMessage, Task> onEvent);
    }
}