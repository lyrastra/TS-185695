using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events;
using System.Threading.Tasks;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements
{
    public interface IMovementDocumentEventWriter
    {
        Task WriteDocumentImportCompletedAsync(DocumentImportCompleted eventData);
        Task WriteDocumentImportFailedAsync(DocumentImportFailed eventData);
        Task WriteDocumentImportSkippedAsync(DocumentImportSkipped eventData);
    }
}
