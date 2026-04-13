using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public interface IIncomeFromCommissionAgentEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IIncomeFromCommissionAgentEventReaderBuilder OnCreated(Func<IncomeFromCommissionAgentCreated, Task> onEvent);
        IIncomeFromCommissionAgentEventReaderBuilder OnUpdated(Func<IncomeFromCommissionAgentUpdated, Task> onEvent);
        IIncomeFromCommissionAgentEventReaderBuilder OnDeleted(Func<IncomeFromCommissionAgentDeleted, Task> onEvent);
    }
}