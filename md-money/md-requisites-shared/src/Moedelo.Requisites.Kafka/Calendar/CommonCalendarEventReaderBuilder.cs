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
    [InjectAsSingleton(typeof(ICommonCalendarEventReaderBuilder))]
    internal sealed class CommonCalendarEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ICommonCalendarEventReaderBuilder
    {
        public CommonCalendarEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.Calendar.Event.Topic,
                  Topics.Calendar.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICommonCalendarEventReaderBuilder OnChanged(Func<CommonCalendarEventChanged, Task> onEvent)
        {
            OnEvent(onEvent, x => x.WithoutContext());

            return this;
        }
    }
}
