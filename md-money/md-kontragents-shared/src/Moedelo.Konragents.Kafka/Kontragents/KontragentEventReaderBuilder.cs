using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;
using Moedelo.Konragents.Kafka.Abstractions.Kontragent;
using Moedelo.Konragents.Kafka.Abstractions.Kontragent.Events;
using Moedelo.Konragents.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Konragents.Kafka.Kontragents
{
    [InjectAsSingleton(typeof(IKontragentEventReaderBuilder))]
    class KontragentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IKontragentEventReaderBuilder
    {
        public KontragentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                KontragentsTopics.Kontragent.Event.Topic,
                KontragentsTopics.Kontragent.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IKontragentEventReaderBuilder OnCreated(Func<KontragentCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnCreated(Func<KontragentCreated, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnUpdated(Func<KontragentUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnUpdated(Func<KontragentUpdated, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnArchived(Func<KontragentArchived, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnArchived(Func<KontragentArchived, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnDearchived(Func<KontragentDearchived, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnDearchived(Func<KontragentDearchived, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnDeleted(Func<KontragentDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IKontragentEventReaderBuilder OnDeleted(Func<KontragentDeleted, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
