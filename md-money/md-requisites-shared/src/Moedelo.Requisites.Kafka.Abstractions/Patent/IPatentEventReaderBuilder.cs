using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Requisites.Kafka.Abstractions.Patent.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Requisites.Kafka.Abstractions.Patent
{
    public interface IPatentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IPatentEventReaderBuilder OnChange(Func<PatentDataChanged, Task> onEvent);

        IPatentEventReaderBuilder OnRemove(Func<PatentDataRemove, Task> onEvent);

        IPatentEventReaderBuilder OnStop(Func<PatentDataStopped, Task> onEvent);
    }
}