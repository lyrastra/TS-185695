using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Stock.Kafka.Abstractions.Products;
using Moedelo.Stock.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Stock.Kafka.Abstractions.Products.Events;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Stock.Kafka.Products
{
    [InjectAsSingleton(typeof(IStockProductEventReaderBuilder))]
    class StockProductEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IStockProductEventReaderBuilder
    {
        public StockProductEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                StockTopics.Product.Event.Topic,
                StockTopics.Product.Entity,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IStockProductEventReaderBuilder OnCreated(Func<StockProductCreatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder OnCreated(Func<StockProductCreatedMessage, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder OnUpdated(Func<StockProductUpdatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder OnUpdated(Func<StockProductUpdatedMessage, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder Ondeleted(Func<StockProductDeletedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder Ondeleted(Func<StockProductDeletedMessage, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder OnMerged(Func<StockProductMergedMessage, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IStockProductEventReaderBuilder OnMerged(Func<StockProductMergedMessage, KafkaMessageValueMetadata, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
