using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract
{
    public interface IAgencyContractEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IAgencyContractEventReaderBuilder OnCreated(Func<AgencyContractCreated, Task> onEvent);
        IAgencyContractEventReaderBuilder OnUpdated(Func<AgencyContractUpdated, Task> onEvent);
        IAgencyContractEventReaderBuilder OnDeleted(Func<AgencyContractDeleted, Task> onEvent);

        IAgencyContractEventReaderBuilder OnProvideRequired(Func<AgencyContractProvideRequired, Task> onEvent);
    }
}