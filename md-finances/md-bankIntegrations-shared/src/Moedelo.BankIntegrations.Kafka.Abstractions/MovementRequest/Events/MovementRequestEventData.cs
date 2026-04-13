using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest.Events
{
    /// <summary>
    /// Событие о завершёнии запроса выписки
    /// </summary>
    public class MovementRequestEventData : IEntityEventData
    {
        public int FirmId { get; set; }
        public string MongoFileId { get; set; }
        public bool IsManual { get; set; }
        public string BankBik { get; set; }
    }
}
