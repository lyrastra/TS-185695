using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.AgencyContract.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.AgencyContract;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(IAgencyContractEventReaderBuilder))]
    class AgencyContractEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAgencyContractEventReaderBuilder
    {
        public AgencyContractEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.AgencyContract.Event.Topic,
                MoneyTopics.CashOrders.AgencyContract.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IAgencyContractEventReaderBuilder OnCreated(Func<AgencyContractCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAgencyContractEventReaderBuilder OnUpdated(Func<AgencyContractUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAgencyContractEventReaderBuilder OnDeleted(Func<AgencyContractDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
