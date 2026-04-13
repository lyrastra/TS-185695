using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionOfOwnFunds.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionOfOwnFunds
{
    /// <summary>
    /// ПКО - "Взнос собственных средств". Чтение событий
    /// </summary>
    public interface IContributionOfOwnFundsEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IContributionOfOwnFundsEventReaderBuilder OnCreated(Func<ContributionOfOwnFundsCreated, Task> onEvent);

        IContributionOfOwnFundsEventReaderBuilder OnUpdated(Func<ContributionOfOwnFundsUpdated, Task> onEvent);

        IContributionOfOwnFundsEventReaderBuilder OnDeleted(Func<ContributionOfOwnFundsDeleted, Task> onEvent);
    }
}
