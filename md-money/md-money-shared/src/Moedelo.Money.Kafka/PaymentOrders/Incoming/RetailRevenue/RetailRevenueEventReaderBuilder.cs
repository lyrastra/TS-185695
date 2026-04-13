using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueEventReaderBuilder))]
    public class RetailRevenueEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRetailRevenueEventReaderBuilder
    {
        public RetailRevenueEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.RetailRevenue.Event.Topic,
                  MoneyTopics.PaymentOrders.RetailRevenue.EntityName,
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

        public IRetailRevenueEventReaderBuilder OnProvideRequired(Func<RetailRevenueProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRetailRevenueEventReaderBuilder OnUpdateAfterAccountingStatementCreated(Func<RetailRevenueUpdateAfterAccountingStatementCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}