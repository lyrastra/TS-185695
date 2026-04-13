using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(IAgencyContractEventReaderBuilder))]
    public class AgencyContractEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAgencyContractEventReaderBuilder
    {
        public AgencyContractEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.AgencyContract.Event.Topic,
                  MoneyTopics.PaymentOrders.AgencyContract.EntityName,
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

        public IAgencyContractEventReaderBuilder OnProvideRequired(Func<AgencyContractProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}