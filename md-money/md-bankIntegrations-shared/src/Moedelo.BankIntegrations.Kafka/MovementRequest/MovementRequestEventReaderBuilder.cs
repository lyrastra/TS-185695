using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest;
using Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest.Events;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.BankIntegrations.Kafka.MovementRequest
{
    [InjectAsSingleton]
    internal sealed class MovementRequestEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IMovementRequestEventReaderBuilder
    {
        public MovementRequestEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.MovementRequestEntity.Event.Topic,
                  Topics.MovementRequestEntity.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IMovementRequestEventReaderBuilder OnRequestFinished(Func<MovementRequestEventData, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IMovementRequestEventReaderBuilder OnReviseRequestFinished(Func<ReviseMovementRequestEventData, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IMovementRequestEventReaderBuilder OnSilentRequestFinished(Func<SilentMovementRequestEventData, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}