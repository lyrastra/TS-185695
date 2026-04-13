using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest.Events
{
    /// <summary>Запрос выписки без уведомления пользователя</summary>
    public class SilentMovementRequestEventData : IEntityEventData
    {
        public int FirmId { get; set; }
        public string MongoFileId { get; set; }
    }
}
