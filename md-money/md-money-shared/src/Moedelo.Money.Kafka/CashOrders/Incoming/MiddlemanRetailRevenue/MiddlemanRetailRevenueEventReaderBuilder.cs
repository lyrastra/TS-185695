using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.MiddlemanRetailRevenue
{
    [InjectAsSingleton(typeof(IMiddlemanRetailRevenueEventReaderBuilder))]
    class MiddlemanRetailRevenueEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IMiddlemanRetailRevenueEventReaderBuilder
    {
        public MiddlemanRetailRevenueEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.MiddlemanRetailRevenue.Event.Topic,
                MoneyTopics.CashOrders.MiddlemanRetailRevenue.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IMiddlemanRetailRevenueEventReaderBuilder OnCreated(Func<MiddlemanRetailRevenueCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IMiddlemanRetailRevenueEventReaderBuilder OnUpdated(Func<MiddlemanRetailRevenueUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IMiddlemanRetailRevenueEventReaderBuilder OnDeleted(Func<MiddlemanRetailRevenueDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
