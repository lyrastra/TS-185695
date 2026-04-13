using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.AgencyContract.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.AgencyContract
{
    /// <summary>
    /// РКО - "Выплата по агентскому договору". Чтение событий
    /// </summary>
    public interface IAgencyContractEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAgencyContractEventReaderBuilder OnCreated(Func<AgencyContractCreated, Task> onEvent);

        IAgencyContractEventReaderBuilder OnUpdated(Func<AgencyContractUpdated, Task> onEvent);

        IAgencyContractEventReaderBuilder OnDeleted(Func<AgencyContractDeleted, Task> onEvent);
    }
}
