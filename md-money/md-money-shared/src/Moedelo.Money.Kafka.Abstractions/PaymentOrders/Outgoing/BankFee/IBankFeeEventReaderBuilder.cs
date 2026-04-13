using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IBankFeeEventReaderBuilder OnCreated(Func<BankFeeCreated, Task> onEvent);
        IBankFeeEventReaderBuilder OnUpdated(Func<BankFeeUpdated, Task> onEvent);
        IBankFeeEventReaderBuilder OnDeleted(Func<BankFeeDeleted, Task> onEvent);

        IBankFeeEventReaderBuilder OnProvideRequired(Func<BankFeeProvideRequired, Task> onEvent);
    }
}