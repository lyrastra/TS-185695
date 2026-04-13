using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.BankBalanceHistory.Events;

namespace Moedelo.Money.Kafka.Abstractions.BankBalanceHistory
{
    public interface IBankBalanceHistoryEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IBankBalanceHistoryEventReaderBuilder OnProcessed(Func<MovementProcessed, Task> onEvent);
    }
}
