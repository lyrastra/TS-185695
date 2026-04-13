using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.TransferFromSettlementAccount
{
    [InjectAsSingleton(typeof(ITransferFromSettlementAccountEventReaderBuilder))]
    class TransferFromSettlementAccountEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ITransferFromSettlementAccountEventReaderBuilder
    {
        public TransferFromSettlementAccountEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.TransferFromSettlementAccount.Event.Topic,
                MoneyTopics.CashOrders.TransferFromSettlementAccount.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ITransferFromSettlementAccountEventReaderBuilder OnCreated(Func<TransferFromSettlementAccountCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferFromSettlementAccountEventReaderBuilder OnUpdated(Func<TransferFromSettlementAccountUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferFromSettlementAccountEventReaderBuilder OnDeleted(Func<TransferFromSettlementAccountDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
