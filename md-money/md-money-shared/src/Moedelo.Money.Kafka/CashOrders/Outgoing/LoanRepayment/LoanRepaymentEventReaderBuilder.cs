using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.LoanRepayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(ILoanRepaymentEventReaderBuilder))]
    class LoanRepaymentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ILoanRepaymentEventReaderBuilder
    {
        public LoanRepaymentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(                
                MoneyTopics.CashOrders.LoanRepayment.Event.Topic,
                MoneyTopics.CashOrders.LoanRepayment.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ILoanRepaymentEventReaderBuilder OnCreated(Func<LoanRepaymentCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanRepaymentEventReaderBuilder OnUpdated(Func<LoanRepaymentUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanRepaymentEventReaderBuilder OnDeleted(Func<LoanRepaymentDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
