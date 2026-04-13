using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee
{
    /// <summary>
    /// Платежные системы - "Удержание комиссии". Чтение событий
    /// </summary>
    public interface IWithholdingOfFeeEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IWithholdingOfFeeEventReaderBuilder OnCreated(Func<WithholdingOfFeeCreated, Task> onEvent);

        IWithholdingOfFeeEventReaderBuilder OnUpdated(Func<WithholdingOfFeeUpdated, Task> onEvent);

        IWithholdingOfFeeEventReaderBuilder OnDeleted(Func<WithholdingOfFeeDeleted, Task> onEvent);
    }
}
