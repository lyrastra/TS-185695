using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IIncomeFromCommissionAgentEventReaderBuilder))]
    class IncomeFromCommissionAgentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IIncomeFromCommissionAgentEventReaderBuilder
    {
        public IncomeFromCommissionAgentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.PaymentOrders.IncomeFromCommissionAgent.Event.Topic,
                MoneyTopics.PaymentOrders.IncomeFromCommissionAgent.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IIncomeFromCommissionAgentEventReaderBuilder OnCreated(Func<IncomeFromCommissionAgentCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IIncomeFromCommissionAgentEventReaderBuilder OnUpdated(Func<IncomeFromCommissionAgentUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IIncomeFromCommissionAgentEventReaderBuilder OnDeleted(Func<IncomeFromCommissionAgentDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
