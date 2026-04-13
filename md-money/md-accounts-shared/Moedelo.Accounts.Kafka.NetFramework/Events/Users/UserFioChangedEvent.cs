using Moedelo.Accounts.Kafka.Abstractions.Events.Users;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Accounts.Kafka.NetFramework.Events.Users
{
    public sealed class UserFioChangedEvent : UserFioChanged, IEntityEventData
    {}
}
