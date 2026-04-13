using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToSettlementAccount.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToSettlementAccount;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.TransferToSettlementAccount
{
    [InjectAsSingleton(typeof(ITransferToSettlementAccountEventReaderBuilder))]
    class TransferToSettlementAccountEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ITransferToSettlementAccountEventReaderBuilder
    {
        public TransferToSettlementAccountEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.TransferToSettlementAccount.Event.Topic,
                MoneyTopics.CashOrders.TransferToSettlementAccount.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ITransferToSettlementAccountEventReaderBuilder OnCreated(Func<TransferToSettlementAccountCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToSettlementAccountEventReaderBuilder OnUpdated(Func<TransferToSettlementAccountUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToSettlementAccountEventReaderBuilder OnDeleted(Func<TransferToSettlementAccountDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
