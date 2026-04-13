using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Contracts.Kafka.Abstractions;
using Moedelo.Contracts.Kafka.Abstractions.Contracts.Events;
using Moedelo.Contracts.Kafka.Abstractions.Contracts.Readers;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Contracts.Kafka.Contracts.Readers;

[InjectAsSingleton(typeof(IContractEventReaderBuilder))]
public class ContractEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IContractEventReaderBuilder
{
    public ContractEventReaderBuilder(
        IMoedeloEntityEventKafkaTopicReader reader,
        IExecutionInfoContextInitializer contextInitializer,
        IExecutionInfoContextAccessor contextAccessor,
        IAuditTracer auditTracer,
        ILogger<ContractEventReaderBuilder> logger)
        : base(
            ContactsTopics.Event.Contract.EventTopic,
            ContactsTopics.Event.Contract.EntityName,
            reader,
            contextInitializer,
            contextAccessor,
            auditTracer,
            logger)
    {}

    public IContractEventReaderBuilder OnContractCreated(Func<ContractCreated, Task> onEvent) => 
        OnContractEvent<ContractCreated>((message, _) => onEvent(message));

    public IContractEventReaderBuilder OnContractArchived(Func<ContractArchived, Task> onEvent) =>
        OnContractEvent<ContractArchived>((message, _) => onEvent(message));

    public IContractEventReaderBuilder OnContractCreated(Func<ContractCreated, KafkaMessageValueMetadata, Task> onEvent) =>
        OnContractEvent(onEvent);

    public IContractEventReaderBuilder OnContractUpdated(Func<ContractUpdated, Task> onEvent) =>
        OnContractEvent<ContractUpdated>((message, _) => onEvent(message));

    public IContractEventReaderBuilder OnContractDeleted(Func<ContractDeleted, Task> onEvent) =>
        OnContractEvent<ContractDeleted>((message, _) => onEvent(message));

    private IContractEventReaderBuilder OnContractEvent<TEvent>(Func<TEvent, KafkaMessageValueMetadata, Task> onEvent) 
        where TEvent : IEntityEventData
    {
        OnEvent<TEvent>((message, metadata) =>
        {
            logger.LogInformationExtraData(
                message,
                $"Обработка события {typeof(TEvent).Name}");

            return onEvent(message, metadata);
        });
        return this;
    }
}