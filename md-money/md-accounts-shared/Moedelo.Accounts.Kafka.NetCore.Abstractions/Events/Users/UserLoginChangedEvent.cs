using Moedelo.Accounts.Kafka.Abstractions.Events.Users;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Users
{
    /// <summary>
    /// Событие "У пользователя изменён логин" для .net core
    /// </summary>
    public sealed class UserLoginChangedEvent : UserLoginChanged, IEntityEventData {}
}