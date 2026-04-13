using Moedelo.Accounts.Kafka.Abstractions.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Accounts
{
    /// <summary>
    /// Реализация события "Из аккаунта была удалена фирма" для .net core
    /// </summary>
    public sealed class FirmWasRemovedFromAccountEvent : FirmWasRemovedFromAccountEventFields,
        IEntityEventData
    {
    }
}