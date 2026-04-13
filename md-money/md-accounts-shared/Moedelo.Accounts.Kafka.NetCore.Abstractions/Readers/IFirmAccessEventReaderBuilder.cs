using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.FirmAccess;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers
{
    public interface IFirmAccessEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        /// <summary>
        /// Подписка на событие "Получение доступа к фирме"
        /// </summary>
        IFirmAccessEventReaderBuilder OnFirmAccessGrantedEvent(Func<FirmAccessGrantedEvent, Task> onEvent);

        /// <summary>
        /// Подписка на событие "Получение доступа к фирме"
        /// </summary>
        IFirmAccessEventReaderBuilder OnFirmAccessGrantedEvent(Func<FirmAccessGrantedEvent, KafkaMessageValueMetadata, Task> onEvent);

        /// <summary>
        /// Событие "Изменение доступа к фирме"
        /// </summary>
        IFirmAccessEventReaderBuilder OnFirmAccessChangedEvent(Func<FirmAccessChangedEvent, Task> onEvent);

        /// <summary>
        /// Событие "Изменение доступа к фирме"
        /// </summary>
        IFirmAccessEventReaderBuilder OnFirmAccessChangedEvent(Func<FirmAccessChangedEvent, KafkaMessageValueMetadata, Task> onEvent);

        /// <summary>
        /// Событие "Доступ к фирме отозван"
        /// </summary>
        IFirmAccessEventReaderBuilder OnFirmAccessRevokedEvent(Func<FirmAccessRevokedEvent, Task> onEvent);

        /// <summary>
        /// Событие "Доступ к фирме отозван"
        /// </summary>
        IFirmAccessEventReaderBuilder OnFirmAccessRevokedEvent(Func<FirmAccessRevokedEvent, KafkaMessageValueMetadata, Task> onEvent);
    }
}