using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.Registry.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.Registry
{
    public interface IRegistryOperationEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRegistryOperationEventReaderBuilder OnCreated(Func<OperationCreated, Task> onEvent);
        IRegistryOperationEventReaderBuilder OnUpdated(Func<OperationUpdated, Task> onEvent);
        IRegistryOperationEventReaderBuilder OnDeleted(Func<OperationDeleted, Task> onEvent);
    }
}
