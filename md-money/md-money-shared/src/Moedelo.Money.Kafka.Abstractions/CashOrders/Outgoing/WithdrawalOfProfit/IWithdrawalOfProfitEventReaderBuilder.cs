using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.WithdrawalOfProfit.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.WithdrawalOfProfit
{
    /// <summary>
    /// РКО - "Снятие прибыли". Чтение событий
    /// </summary>
    public interface IWithdrawalOfProfitEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IWithdrawalOfProfitEventReaderBuilder OnCreated(Func<WithdrawalOfProfitCreated, Task> onEvent);

        IWithdrawalOfProfitEventReaderBuilder OnUpdated(Func<WithdrawalOfProfitUpdated, Task> onEvent);

        IWithdrawalOfProfitEventReaderBuilder OnDeleted(Func<WithdrawalOfProfitDeleted, Task> onEvent);
    }
}
