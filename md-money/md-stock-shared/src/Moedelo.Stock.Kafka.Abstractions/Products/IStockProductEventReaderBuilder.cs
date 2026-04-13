using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Stock.Kafka.Abstractions.Products.Events;

namespace Moedelo.Stock.Kafka.Abstractions.Products
{
    public interface IStockProductEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IStockProductEventReaderBuilder OnCreated(Func<StockProductCreatedMessage, Task> onEvent);
        IStockProductEventReaderBuilder OnCreated(Func<StockProductCreatedMessage, KafkaMessageValueMetadata, Task> onEvent);

        IStockProductEventReaderBuilder OnUpdated(Func<StockProductUpdatedMessage, Task> onEvent);
        IStockProductEventReaderBuilder OnUpdated(Func<StockProductUpdatedMessage, KafkaMessageValueMetadata, Task> onEvent);

        IStockProductEventReaderBuilder Ondeleted(Func<StockProductDeletedMessage, Task> onEvent);
        IStockProductEventReaderBuilder Ondeleted(Func<StockProductDeletedMessage, KafkaMessageValueMetadata, Task> onEvent);

        IStockProductEventReaderBuilder OnMerged(Func<StockProductMergedMessage, Task> onEvent);
        IStockProductEventReaderBuilder OnMerged(Func<StockProductMergedMessage, KafkaMessageValueMetadata, Task> onEvent);
    }
}
