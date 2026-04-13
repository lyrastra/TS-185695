using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest.Events
{
    /// <summary>
    /// Событие о завершёнии запроса выписки на сверку
    /// </summary>
    public class ReviseMovementRequestEventData : IEntityEventData
    {
        public int FirmId { get; set; }
        public string FileId { get; set; }
        public bool IsManual { get; set; }
        public string BankBik { get; set; }
    }
}
