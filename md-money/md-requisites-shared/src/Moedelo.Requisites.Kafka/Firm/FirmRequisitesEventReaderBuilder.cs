using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Requisites.Kafka.Abstractions.Firm;
using Moedelo.Requisites.Kafka.Abstractions.Firm.Events;

namespace Moedelo.Requisites.Kafka.Firm
{
    [InjectAsSingleton]
    internal sealed class FirmRequisitesEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder,
        IFirmRequisitesEventReaderBuilder
    {
        public FirmRequisitesEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.FirmEntity.Event.Topic,
                Topics.FirmEntity.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IFirmRequisitesEventReaderBuilder OnChangedUserLogin(
            Func<UserLoginChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedRegistrationData(
            Func<RegistrationDataChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedRegistrationData(
            Func<RegistrationDataChanged, KafkaMessageValueMetadata, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedOpf(
            Func<OpfChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedDirector(
            Func<DirectorChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedAddress(
            Func<AddressChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedEmployerMode(
            Func<EmployerModeChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedIpData(
            Func<IpPassportDataChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmRequisitesEventReaderBuilder OnChangedRequisites(
            Func<RequisitesChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }
    }
}