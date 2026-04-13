using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.FinancialAssistance.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(IFinancialAssistanceEventReaderBuilder))]
    class FinancialAssistanceEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IFinancialAssistanceEventReaderBuilder
    {
        public FinancialAssistanceEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.FinancialAssistance.Event.Topic,
                MoneyTopics.CashOrders.FinancialAssistance.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IFinancialAssistanceEventReaderBuilder OnCreated(Func<FinancialAssistanceCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IFinancialAssistanceEventReaderBuilder OnUpdated(Func<FinancialAssistanceUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IFinancialAssistanceEventReaderBuilder OnDeleted(Func<FinancialAssistanceDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
