using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashEventReaderBuilder))]
    class TransferFromCashEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ITransferFromCashEventReaderBuilder
    {
        public TransferFromCashEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.TransferFromCash.Event.Topic,
                MoneyTopics.CashOrders.TransferFromCash.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ITransferFromCashEventReaderBuilder OnCreated(Func<TransferFromCashCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferFromCashEventReaderBuilder OnUpdated(Func<TransferFromCashUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferFromCashEventReaderBuilder OnDeleted(Func<TransferFromCashDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
