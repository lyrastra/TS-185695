using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Accounts;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers
{
    public interface IAccountEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        /// <summary>
        /// Подписка на событие "Объединение аккаунтов: другой аккаунт был присоединён к этому аккаунту"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        IAccountEventReaderBuilder OnAccountWasMergedWithAnotherAccountEvent(Func<AccountWasMergedWithAnotherAccountEvent, Task> onEvent);
        /// <summary>
        /// Подписка на событие "Из аккаунта была удалена фирма"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        IAccountEventReaderBuilder OnFirmWasRemovedFromAccountEvent(Func<FirmWasRemovedFromAccountEvent, Task> onEvent);
        /// <summary>
        /// Подписка на событие "В аккаунт была добавлена новая фирма"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        IAccountEventReaderBuilder OnNewFirmWasCreatedInAccountEvent(Func<NewFirmWasCreatedInAccountEvent, Task> onEvent);
        /// <summary>
        /// Подписка на событие "В аккаунт был добавлен новый пользователь"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        IAccountEventReaderBuilder OnNewUserWasCreatedInAccountEvent(Func<NewUserWasCreatedInAccountEvent, Task> onEvent);
    }
}