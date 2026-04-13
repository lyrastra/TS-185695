using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Stock.Kafka.Abstractions.Products.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Stock.Kafka.Abstractions.Operations
{
    public interface IStockOperationEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IStockOperationEventReaderBuilder OnCreated(Func<StockOperationCreated, Task> onEvent);

        IStockOperationEventReaderBuilder OnUpdated(Func<StockOperationUpdated, Task> onEvent);
        IStockOperationEventReaderBuilder OnDeleted(Func<StockOperationDeleted, Task> onEvent);
    }
}
