using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ILoanRepaymentEventReaderBuilder OnCreated(Func<LoanRepaymentCreated, Task> onEvent);
        ILoanRepaymentEventReaderBuilder OnUpdated(Func<LoanRepaymentUpdated, Task> onEvent);
        ILoanRepaymentEventReaderBuilder OnDeleted(Func<LoanRepaymentDeleted, Task> onEvent);

        ILoanRepaymentEventReaderBuilder OnProvideRequired(Func<LoanRepaymentProvideRequired, Task> onEvent);
    }
}