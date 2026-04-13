using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.Maintenance;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers
{
    public interface IFirmMaintenanceEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IFirmMaintenanceEventReaderBuilder OnFirmsWereDeletedEvent(
            Func<FirmsWereDeletedEvent, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);
    }
}