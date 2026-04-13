using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue
{
    [InjectAsSingleton(typeof(ILoanIssueEventReaderBuilder))]
    public class LoanIssueEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ILoanIssueEventReaderBuilder
    {
        public LoanIssueEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.LoanIssue.Event.Topic,
                  MoneyTopics.PaymentOrders.LoanIssue.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ILoanIssueEventReaderBuilder OnCreated(Func<LoanIssueCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanIssueEventReaderBuilder OnUpdated(Func<LoanIssueUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanIssueEventReaderBuilder OnDeleted(Func<LoanIssueDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ILoanIssueEventReaderBuilder OnProvideRequired(Func<LoanIssueProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}