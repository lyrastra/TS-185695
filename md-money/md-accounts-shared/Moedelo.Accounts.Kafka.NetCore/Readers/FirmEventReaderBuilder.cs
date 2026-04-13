using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.Abstractions;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Firm;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using FirmIsInternalChangedEvent = Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Firm.FirmIsInternalChangedEvent;

namespace Moedelo.Accounts.Kafka.NetCore.Readers
{
    [InjectAsSingleton(typeof(IFirmEventReaderBuilder))]
    public sealed class FirmEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IFirmEventReaderBuilder
    {
        public FirmEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.Firm.Event.Topic,
                Topics.Firm.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }
        
        /// <summary>
        /// Подписка на событие "Смена флага IsInternal"
        /// </summary>
        /// <param name="onEvent">Обработчик события</param>
        public IFirmEventReaderBuilder OnFirmIsInternalChangedEvent(Func<FirmIsInternalChangedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        /// <summary>
        /// Подписка на событие удаление аккаунта из реквизитов
        /// Физического удаления не происходит, только проставляется флаг is_deleted у фирмы и у пользователя
        /// </summary>
        /// <param name="onEvent"></param>
        /// <returns>Обработчик события</returns>
        public IFirmEventReaderBuilder OnFirmMarkedIsDeletedEvent(Func<FirmMarkedIsDeletedEvent, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}