using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest
{
    public interface IMovementRequestEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IMovementRequestEventReaderBuilder OnRequestFinished(Func<MovementRequestEventData, Task> onEvent);

        IMovementRequestEventReaderBuilder OnReviseRequestFinished(Func<ReviseMovementRequestEventData, Task> onEvent);

        IMovementRequestEventReaderBuilder OnSilentRequestFinished(Func<SilentMovementRequestEventData, Task> onEvent);

    }
}