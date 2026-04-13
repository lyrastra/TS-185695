using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueEventReaderBuilder))]
    class RetailRevenueEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRetailRevenueEventReaderBuilder
    {
        public RetailRevenueEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.RetailRevenue.Event.Topic,
                MoneyTopics.CashOrders.RetailRevenue.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IRetailRevenueEventReaderBuilder OnCreated(Func<RetailRevenueCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRetailRevenueEventReaderBuilder OnUpdated(Func<RetailRevenueUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRetailRevenueEventReaderBuilder OnDeleted(Func<RetailRevenueDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
