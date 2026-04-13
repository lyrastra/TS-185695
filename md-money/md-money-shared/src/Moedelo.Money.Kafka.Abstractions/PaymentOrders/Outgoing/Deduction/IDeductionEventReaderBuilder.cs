using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Events;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction
{
    public interface IDeductionEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IDeductionEventReaderBuilder OnCreated(Func<DeductionCreated, Task> onEvent);
        IDeductionEventReaderBuilder OnUpdated(Func<DeductionUpdated, Task> onEvent);
        IDeductionEventReaderBuilder OnDeleted(Func<DeductionDeleted, Task> onEvent);
        IDeductionEventReaderBuilder OnProvideRequired(Func<DeductionProvideRequired, Task> onEvent);
    }
}