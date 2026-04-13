using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToCash.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToCash;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.TransferToCash
{
    [InjectAsSingleton(typeof(ITransferToCashEventReaderBuilder))]
    class TransferToCashEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ITransferToCashEventReaderBuilder
    {
        public TransferToCashEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.TransferToCash.Event.Topic,
                MoneyTopics.CashOrders.TransferToCash.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ITransferToCashEventReaderBuilder OnCreated(Func<TransferToCashCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToCashEventReaderBuilder OnUpdated(Func<TransferToCashUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToCashEventReaderBuilder OnDeleted(Func<TransferToCashDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
