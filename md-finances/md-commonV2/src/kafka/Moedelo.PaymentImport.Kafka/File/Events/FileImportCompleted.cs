using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.PaymentImport.Kafka.File.Events
{
    public class FileImportCompleted : IEntityEventData
    {
        public string SourceFileId { get; set; }
    }
}
