using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Stock.Kafka.Abstractions.Operations;
using Moedelo.Stock.Kafka.Abstractions.Products.Events;
using Moedelo.Stock.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Stock.Kafka.Operations
{
    [InjectAsSingleton(typeof(IStockOperationEventReaderBuilder))]
    class StockOperationEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IStockOperationEventReaderBuilder
    {
        public StockOperationEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                StockTopics.Operation.Event.Topic,
                StockTopics.Operation.Entity,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IStockOperationEventReaderBuilder OnCreated(Func<StockOperationCreated, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IStockOperationEventReaderBuilder OnUpdated(Func<StockOperationUpdated, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IStockOperationEventReaderBuilder OnDeleted(Func<StockOperationDeleted, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}
