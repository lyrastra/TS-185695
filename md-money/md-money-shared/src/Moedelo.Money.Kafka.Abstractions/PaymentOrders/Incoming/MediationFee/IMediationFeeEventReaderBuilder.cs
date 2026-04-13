using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IMediationFeeEventReaderBuilder OnCreated(Func<MediationFeeCreated, Task> onEvent);
        IMediationFeeEventReaderBuilder OnUpdated(Func<MediationFeeUpdated, Task> onEvent);
        IMediationFeeEventReaderBuilder OnDeleted(Func<MediationFeeDeleted, Task> onEvent);

        IMediationFeeEventReaderBuilder OnProvideRequired(Func<MediationFeeProvideRequired, Task> onEvent);
    }
}