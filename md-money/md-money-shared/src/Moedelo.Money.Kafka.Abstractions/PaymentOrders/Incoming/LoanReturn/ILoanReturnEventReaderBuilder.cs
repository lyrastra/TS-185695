using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ILoanReturnEventReaderBuilder OnCreated(Func<LoanReturnCreated, Task> onEvent);
        ILoanReturnEventReaderBuilder OnUpdated(Func<LoanReturnUpdated, Task> onEvent);
        ILoanReturnEventReaderBuilder OnDeleted(Func<LoanReturnDeleted, Task> onEvent);

        ILoanReturnEventReaderBuilder OnProvideRequired(Func<LoanReturnProvideRequired, Task> onEvent);
    }
}