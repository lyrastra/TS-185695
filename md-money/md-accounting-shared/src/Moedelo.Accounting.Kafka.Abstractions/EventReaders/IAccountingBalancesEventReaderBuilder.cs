using System;
using System.Threading.Tasks;
using Moedelo.Accounting.Kafka.Abstractions.Events.EventData.AccountingBalances;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounting.Kafka.Abstractions.EventReaders
{
    public interface IAccountingBalancesEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAccountingBalancesEventReaderBuilder OnMinDateChanged(Func<AccountingBalancesMinDateChangedMessage, Task> onEvent);
    }
}