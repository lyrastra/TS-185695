using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.Kafka.Abstractions.StockVisibility;
using Moedelo.Requisites.Kafka.Abstractions.StockVisibility.Events;
using Topics = Moedelo.Requisites.Kafka.Abstractions.StockVisibility.Topics;

namespace Moedelo.Requisites.Kafka.StockVisibility
{
    [InjectAsSingleton]
    internal sealed class StockVisibilityEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IStockVisibilityEventReaderBuilder
    {
        public StockVisibilityEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.StockVisibilityEntity.Event.Topic, 
                Topics.StockVisibilityEntity.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }


        public IStockVisibilityEventReaderBuilder OnChanged(Func<StockVisibilityChanged, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}