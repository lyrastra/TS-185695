using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentToNaturalPersonsEventReaderBuilder OnCreated(Func<PaymentToNaturalPersonsCreated, Task> onEvent);
        IPaymentToNaturalPersonsEventReaderBuilder OnUpdated(Func<PaymentToNaturalPersonsUpdated, Task> onEvent);
        IPaymentToNaturalPersonsEventReaderBuilder OnDeleted(Func<PaymentToNaturalPersonsDeleted, Task> onEvent);

        IPaymentToNaturalPersonsEventReaderBuilder OnProvideRequired(Func<PaymentToNaturalPersonsProvideRequired, Task> onEvent);
    }
}