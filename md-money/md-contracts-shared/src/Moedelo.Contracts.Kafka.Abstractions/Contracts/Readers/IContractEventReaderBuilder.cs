using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Contracts.Kafka.Abstractions.Contracts.Events;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Contracts.Kafka.Abstractions.Contracts.Readers;

public interface IContractEventReaderBuilder: IMoedeloEntityEventKafkaTopicReaderBuilder
{
    IContractEventReaderBuilder OnContractCreated(Func<ContractCreated, Task> onEvent);
    IContractEventReaderBuilder OnContractUpdated(Func<ContractUpdated, Task> onEvent);
    IContractEventReaderBuilder OnContractDeleted(Func<ContractDeleted, Task> onEvent);
    IContractEventReaderBuilder OnContractArchived(Func<ContractArchived, Task> onEvent);
    IContractEventReaderBuilder OnContractCreated(Func<ContractCreated, KafkaMessageValueMetadata, Task> onEvent);
}