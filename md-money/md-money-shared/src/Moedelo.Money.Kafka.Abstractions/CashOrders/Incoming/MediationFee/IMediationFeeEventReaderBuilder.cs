using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MediationFee.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MediationFee
{
    /// <summary>
    /// ПКО - "Посредническое вознаграждение". Чтение событий
    /// </summary>
    public interface IMediationFeeEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IMediationFeeEventReaderBuilder OnCreated(Func<MediationFeeCreated, Task> onEvent);

        IMediationFeeEventReaderBuilder OnUpdated(Func<MediationFeeUpdated, Task> onEvent);

        IMediationFeeEventReaderBuilder OnDeleted(Func<MediationFeeDeleted, Task> onEvent);
    }
}
