using Moedelo.BankIntegrations.Kafka.Abstractions.IntegratedUser;
using Moedelo.BankIntegrations.Kafka.Abstractions.IntegratedUser.Events;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.Kafka.IntegratedUser
{
    [InjectAsSingleton]
    internal sealed class IntegratedUserEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IIntegratedUserEventReaderBuilder
    {
        public IntegratedUserEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.IntegratedUserEntity.Event.Topic,
                  Topics.IntegratedUserEntity.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IIntegratedUserEventReaderBuilder OnIntegratedUserChanged(Func<IntegratedUserEventData, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}