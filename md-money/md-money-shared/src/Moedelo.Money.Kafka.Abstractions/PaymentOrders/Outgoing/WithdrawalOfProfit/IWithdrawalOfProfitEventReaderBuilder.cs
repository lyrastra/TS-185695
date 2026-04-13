using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IWithdrawalOfProfitEventReaderBuilder OnCreated(Func<WithdrawalOfProfitCreated, Task> onEvent);
        IWithdrawalOfProfitEventReaderBuilder OnUpdated(Func<WithdrawalOfProfitUpdated, Task> onEvent);
        IWithdrawalOfProfitEventReaderBuilder OnDeleted(Func<WithdrawalOfProfitDeleted, Task> onEvent);

        IWithdrawalOfProfitEventReaderBuilder OnProvideRequired(Func<WithdrawalOfProfitProvideRequired, Task> onEvent);
    }
}