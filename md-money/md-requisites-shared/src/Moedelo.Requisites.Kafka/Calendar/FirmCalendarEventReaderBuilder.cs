using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.Kafka.Abstractions.Calendar;
using Moedelo.Requisites.Kafka.Abstractions.Calendar.Events;

namespace Moedelo.Requisites.Kafka.Calendar
{
    [InjectAsSingleton]
    internal sealed class FirmCalendarEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IFirmCalendarEventReaderBuilder
    {
        public FirmCalendarEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.FirmCalendar.Event.Topic,
                  Topics.FirmCalendar.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IFirmCalendarEventReaderBuilder OnRebuildDone(Func<RebuildDoneEvent, Task> onEvent,
            Action<IMoedeloEntityEventHandlerDefinition> eventDefinitionAction = null)
        {
            OnEvent(onEvent, eventDefinitionAction);

            return this;
        }

        public IFirmCalendarEventReaderBuilder OnUserCalendarEventCreated(Func<UserCalendarEventCreated, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IFirmCalendarEventReaderBuilder OnUserCalendarEventDeleted(Func<UserCalendarEventDeleted, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}
