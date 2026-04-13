using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Requisites.Kafka.Abstractions.Firm.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Firm
{
    public interface IFirmRequisitesEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IFirmRequisitesEventReaderBuilder OnChangedUserLogin(
            Func<UserLoginChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedRegistrationData(
            Func<RegistrationDataChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedRegistrationData(
            Func<RegistrationDataChanged, KafkaMessageValueMetadata, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedOpf(
            Func<OpfChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedDirector(
            Func<DirectorChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedAddress(
            Func<AddressChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedEmployerMode(
            Func<EmployerModeChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedIpData(
            Func<IpPassportDataChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmRequisitesEventReaderBuilder OnChangedRequisites(
            Func<RequisitesChanged, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);
    }
}