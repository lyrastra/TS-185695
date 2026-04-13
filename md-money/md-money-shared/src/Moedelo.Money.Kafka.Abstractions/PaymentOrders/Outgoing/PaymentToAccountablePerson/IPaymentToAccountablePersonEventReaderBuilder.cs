using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPaymentToAccountablePersonEventReaderBuilder OnCreated(Func<PaymentToAccountablePersonCreated, Task> onEvent);
        IPaymentToAccountablePersonEventReaderBuilder OnUpdated(Func<PaymentToAccountablePersonUpdated, Task> onEvent);
        IPaymentToAccountablePersonEventReaderBuilder OnDeleted(Func<PaymentToAccountablePersonDeleted, Task> onEvent);

        IPaymentToAccountablePersonEventReaderBuilder OnProvideRequired(Func<PaymentToAccountablePersonProvideRequired, Task> onEvent);
    }
}