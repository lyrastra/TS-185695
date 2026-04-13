using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Requisites.Kafka.Abstractions.Calendar.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.Calendar
{
    public interface IFirmCalendarEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IFirmCalendarEventReaderBuilder OnRebuildDone(
            Func<RebuildDoneEvent, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null);

        IFirmCalendarEventReaderBuilder OnUserCalendarEventCreated(Func<UserCalendarEventCreated, Task> onEvent);
        IFirmCalendarEventReaderBuilder OnUserCalendarEventDeleted(Func<UserCalendarEventDeleted, Task> onEvent);
    }
}
