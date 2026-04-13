using Moedelo.Accounts.Kafka.Abstractions.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Accounts
{
    /// <summary>
    /// Реализация события "В аккаунт был добавлен новый пользователь" для .net core
    /// </summary>
    public sealed class NewUserWasCreatedInAccountEvent : NewUserWasCreatedInAccountEventFields, IEntityEventData
    {
    }
}