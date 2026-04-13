using Moedelo.Accounts.Kafka.Abstractions.Events.Maintenance;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events
{
    /// <summary>
    /// Событие "Фирмы были удалены"
    /// </summary>
    public sealed class FirmsWereDeletedEvent : FirmsWereDeleted, IEntityEventData
    {
    }
}