using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital
{
    /// <summary>
    /// ПКО - "Взнос в уставный капитал". Чтение событий
    /// </summary>
    public interface IContributionToAuthorizedCapitalEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IContributionToAuthorizedCapitalEventReaderBuilder OnCreated(Func<ContributionToAuthorizedCapitalCreated, Task> onEvent);

        IContributionToAuthorizedCapitalEventReaderBuilder OnUpdated(Func<ContributionToAuthorizedCapitalUpdated, Task> onEvent);

        IContributionToAuthorizedCapitalEventReaderBuilder OnDeleted(Func<ContributionToAuthorizedCapitalDeleted, Task> onEvent);
    }
}
