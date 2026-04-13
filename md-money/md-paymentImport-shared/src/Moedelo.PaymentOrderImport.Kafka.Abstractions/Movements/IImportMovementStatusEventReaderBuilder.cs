
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using System.Threading.Tasks;
using System;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements
{
    public interface IImportMovementStatusEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IImportMovementStatusEventReaderBuilder OnMovementParsingFailed(Func<MovementParsingFailed, Task> onEvent);

        IImportMovementStatusEventReaderBuilder OnMovementImportCompleted(Func<MovementImportCompleted, Task> onEvent);
    }
}
