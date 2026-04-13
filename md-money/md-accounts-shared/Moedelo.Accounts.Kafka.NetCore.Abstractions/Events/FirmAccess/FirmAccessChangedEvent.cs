using Moedelo.Accounts.Kafka.Abstractions.Events.FirmAccess;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.FirmAccess
{
    /// <summary>
    /// Событие "Изменение доступа к фирме"
    /// </summary>
    public sealed class FirmAccessChangedEvent: FirmAccessChanged, IEntityEventData
    {
    }
}
