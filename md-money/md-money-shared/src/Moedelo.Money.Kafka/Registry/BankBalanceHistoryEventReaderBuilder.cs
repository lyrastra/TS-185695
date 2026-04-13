using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.Registry;
using Moedelo.Money.Kafka.Abstractions.Registry.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.BankBalanceHistory
{
    [InjectAsSingleton(typeof(IRegistryOperationEventReaderBuilder))]
    class RegistryOperationEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRegistryOperationEventReaderBuilder
    {
        public RegistryOperationEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.Registry.Operation.Event.Topic,
                MoneyTopics.Registry.Operation.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IRegistryOperationEventReaderBuilder OnCreated(Func<OperationCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRegistryOperationEventReaderBuilder OnUpdated(Func<OperationUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRegistryOperationEventReaderBuilder OnDeleted(Func<OperationDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
