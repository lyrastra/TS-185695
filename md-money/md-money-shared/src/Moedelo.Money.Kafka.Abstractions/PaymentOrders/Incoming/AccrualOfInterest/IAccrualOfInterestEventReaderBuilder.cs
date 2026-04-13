using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAccrualOfInterestEventReaderBuilder OnCreated(Func<AccrualOfInterestCreated, Task> onEvent);
        IAccrualOfInterestEventReaderBuilder OnUpdated(Func<AccrualOfInterestUpdated, Task> onEvent);
        IAccrualOfInterestEventReaderBuilder OnDeleted(Func<AccrualOfInterestDeleted, Task> onEvent);

        IAccrualOfInterestEventReaderBuilder OnProvideRequired(Func<AccrualOfInterestProvideRequired, Task> onEvent);
    }
}