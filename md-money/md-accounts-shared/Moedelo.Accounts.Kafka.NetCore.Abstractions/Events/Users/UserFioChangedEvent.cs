using Moedelo.Accounts.Kafka.Abstractions.Events.Users;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Users
{
    public class UserFioChangedEvent : UserFioChanged, IEntityEventData
    {
    }
}
