using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements
{
    public interface IImportMovementDocumentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IImportMovementDocumentEventReaderBuilder OnDocumentImportCompleted(Func<DocumentImportCompleted, Task> onEvent);

        IImportMovementDocumentEventReaderBuilder OnDocumentImportFailed(Func<DocumentImportFailed, Task> onEvent);

        IImportMovementDocumentEventReaderBuilder OnDocumentImportSkipped(Func<DocumentImportSkipped, Task> onEvent);
    }
}
