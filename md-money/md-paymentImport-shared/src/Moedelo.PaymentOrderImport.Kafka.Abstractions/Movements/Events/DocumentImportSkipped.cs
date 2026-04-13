using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events
{
    public class DocumentImportSkipped : IEntityEventData
    {
        public int ImportId { get; set; }

        public string SourceFileId { get; set; }

        public bool IsInClosedPeriod { get; set; }

        public int? ImportLogId { get; set; }
    }
}
