using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IWithdrawalFromAccountEventReaderBuilder OnCreated(Func<WithdrawalFromAccountCreated, Task> onEvent);
        IWithdrawalFromAccountEventReaderBuilder OnUpdated(Func<WithdrawalFromAccountUpdated, Task> onEvent);
        IWithdrawalFromAccountEventReaderBuilder OnDeleted(Func<WithdrawalFromAccountDeleted, Task> onEvent);

        IWithdrawalFromAccountEventReaderBuilder OnProvideRequired(Func<WithdrawalFromAccountProvideRequired, Task> onEvent);
    }
}