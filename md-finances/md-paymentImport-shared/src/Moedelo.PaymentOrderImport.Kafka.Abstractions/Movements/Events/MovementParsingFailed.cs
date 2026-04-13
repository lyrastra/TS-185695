using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events
{
    public class MovementParsingFailed : IEntityEventData
    {
        public int ImportId { get; set; }

        public string SourceFileId { get; set; }

        public bool NeedsRetry { get; set; }

        public MovementSource Source { get; set; }

        public string Message { get; set; }
    }
}
