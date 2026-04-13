using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events
{
    public class MovementImportCompleted : IEntityEventData
    {
        public int ImportId { get; set; }
        public MovementSource Source { get; set; }
        public string Message { get; set; }
        public string SourceFileId { get; set; }
    }
}
