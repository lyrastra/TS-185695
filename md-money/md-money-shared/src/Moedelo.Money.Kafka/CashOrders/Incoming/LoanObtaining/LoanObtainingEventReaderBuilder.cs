using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.LoanObtaining;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.LoanObtaining.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.LoanObtaining
{
    [InjectAsSingleton(typeof(ILoanObtainingEventReaderBuilder))]
    class LoanObtainingEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ILoanObtainingEventReaderBuilder
    {
        public LoanObtainingEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.LoanObtaining.Event.Topic,
                MoneyTopics.CashOrders.LoanObtaining.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ILoanObtainingEventReaderBuilder OnCreated(Func<LoanObtainingCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanObtainingEventReaderBuilder OnUpdated(Func<LoanObtainingUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanObtainingEventReaderBuilder OnDeleted(Func<LoanObtainingDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
