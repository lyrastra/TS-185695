using Moedelo.Accounts.Kafka.Abstractions.Events.FirmAccess;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.FirmAccess
{
    /// <summary>
    /// Событие "Доступ к фирме отозван"
    /// </summary>
    public sealed class FirmAccessRevokedEvent: FirmAccessRevoked, IEntityEventData
    {
    }
}
