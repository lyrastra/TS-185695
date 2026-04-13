using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Users;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers
{
    public interface IUserEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IUserEventReaderBuilder OnUserMarkedAsDeletedEvent(Func<UserMarkedAsDeletedEvent, Task> onEvent);
        IUserEventReaderBuilder OnUserLoginChangedEvent(Func<UserLoginChangedEvent, Task> onEvent);
        IUserEventReaderBuilder OnUserBackofficePermissionsChangedEvent(Func<UserBackofficePermissionsChangedEvent, Task> onEvent);
        IUserEventReaderBuilder OnUserFioChangedEvent(Func<UserFioChangedEvent, Task> onEvent);
        IUserEventReaderBuilder OnUserRoleChangedEvent(Func<UserRoleChangedEvent, Task> onEvent);
    }
}