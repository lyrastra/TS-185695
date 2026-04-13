using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.LoanRepayment.Events;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.LoanRepayment
{
    // <summary>
    /// РКО - "Погашение займа или процентов". Чтение событий
    /// </summary>
    public interface ILoanRepaymentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ILoanRepaymentEventReaderBuilder OnCreated(Func<LoanRepaymentCreated, Task> onEvent);

        ILoanRepaymentEventReaderBuilder OnUpdated(Func<LoanRepaymentUpdated, Task> onEvent);

        ILoanRepaymentEventReaderBuilder OnDeleted(Func<LoanRepaymentDeleted, Task> onEvent);
    }
}
