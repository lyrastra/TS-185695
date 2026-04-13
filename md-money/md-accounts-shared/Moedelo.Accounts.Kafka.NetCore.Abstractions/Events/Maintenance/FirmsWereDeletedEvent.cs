using Moedelo.Accounts.Kafka.Abstractions.Events.Maintenance;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Maintenance
{
    /// <summary>
    /// Событие "Фирмы были удалены"
    /// </summary>
    public class FirmsWereDeletedEvent : FirmsWereDeleted, IEntityEventData
    {
    }
}