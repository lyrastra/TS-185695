using System;
using System.Threading.Tasks;
using Moedelo.Accounting.Kafka.Abstractions.Events.EventData.ClosedPeriods;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounting.Kafka.Abstractions.EventReaders
{
    public interface IClosedPeriodEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IClosedPeriodEventReaderBuilder OnCreated(Func<ClosedPeriodCreated, Task> onEvent);
        IClosedPeriodEventReaderBuilder OnDeleted(Func<ClosedPeriodDeleted, Task> onEvent);
        IClosedPeriodEventReaderBuilder OnCheckPeriodCompleted(Func<CheckPeriodCompleted, Task> onEvent);
    }
}