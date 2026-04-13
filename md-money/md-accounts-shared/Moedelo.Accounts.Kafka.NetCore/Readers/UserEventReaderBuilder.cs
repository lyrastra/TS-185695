using Moedelo.Accounts.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Users;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Accounts.Kafka.NetCore.Readers
{
    [InjectAsSingleton(typeof(IUserEventReaderBuilder))]
    public sealed class UserEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IUserEventReaderBuilder
    {
        public UserEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.User.Event.Topic,
                Topics.User.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        /// <summary>
        /// Подписка на событие "Пользователь помечен как удалённый"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        public IUserEventReaderBuilder OnUserMarkedAsDeletedEvent(
            Func<UserMarkedAsDeletedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        /// <summary>
        /// Подписка на событие "У пользователя изменён логин"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        public IUserEventReaderBuilder OnUserLoginChangedEvent(Func<UserLoginChangedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        /// <summary>
        /// Подписка на событие "У пользователя сменились права в бэк-офисе (партнёрке)"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        public IUserEventReaderBuilder OnUserBackofficePermissionsChangedEvent(Func<UserBackofficePermissionsChangedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        /// <summary>
        /// Подписка на событие "У пользователя изменено ФИО"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        public IUserEventReaderBuilder OnUserFioChangedEvent(Func<UserFioChangedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        /// <summary>
        /// Подписка на событие "У пользователя изменена роль"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        public IUserEventReaderBuilder OnUserRoleChangedEvent(Func<UserRoleChangedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}