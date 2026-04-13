using Moedelo.Accounts.Kafka.Abstractions.Events.Firm;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Firm
{
    /// <summary>
    /// Событие "Смена флага IsInternal" у фирмы для .net core
    /// </summary>
    public sealed class FirmIsInternalChangedEvent : FirmIsInternalChanged, IEntityEventData
    {
    }
}