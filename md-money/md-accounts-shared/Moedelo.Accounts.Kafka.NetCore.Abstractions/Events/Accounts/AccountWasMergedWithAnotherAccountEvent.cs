using Moedelo.Accounts.Kafka.Abstractions.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Accounts
{
    /// <summary>
    /// Реализация события "Объединение аккаунтов: другой аккаунт был присоединён к этому аккаунту" для .net core
    /// </summary>
    public sealed class AccountWasMergedWithAnotherAccountEvent : AccountWasMergedWithAnotherAccountEventFields,
        IEntityEventData
    {
    }
}