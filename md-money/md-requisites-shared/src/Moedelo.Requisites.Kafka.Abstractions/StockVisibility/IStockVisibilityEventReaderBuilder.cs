using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Requisites.Kafka.Abstractions.StockVisibility.Events;

namespace Moedelo.Requisites.Kafka.Abstractions.StockVisibility
{
    public interface IStockVisibilityEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IStockVisibilityEventReaderBuilder OnChanged(Func<StockVisibilityChanged, Task> onEvent);
    }
}