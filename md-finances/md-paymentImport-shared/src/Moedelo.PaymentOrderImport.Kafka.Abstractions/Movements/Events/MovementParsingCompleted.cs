using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events
{
    public class MovementParsingCompleted : IEntityEventData
    {
        public int ImportId { get; set; }
        public string SourceFileId { get; set; }
    }
}
