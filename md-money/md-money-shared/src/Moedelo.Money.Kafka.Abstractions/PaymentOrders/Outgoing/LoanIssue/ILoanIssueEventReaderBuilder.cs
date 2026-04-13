using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ILoanIssueEventReaderBuilder OnCreated(Func<LoanIssueCreated, Task> onEvent);
        ILoanIssueEventReaderBuilder OnUpdated(Func<LoanIssueUpdated, Task> onEvent);
        ILoanIssueEventReaderBuilder OnDeleted(Func<LoanIssueDeleted, Task> onEvent);

        ILoanIssueEventReaderBuilder OnProvideRequired(Func<LoanIssueProvideRequired, Task> onEvent);
    }
}