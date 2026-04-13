using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountEventReaderBuilder))]
    public class TransferToAccountEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ITransferToAccountEventReaderBuilder
    {
        public TransferToAccountEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.TransferToAccount.Event.Topic,
                  MoneyTopics.PaymentOrders.TransferToAccount.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ITransferToAccountEventReaderBuilder OnCreated(Func<TransferToAccountCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToAccountEventReaderBuilder OnUpdated(Func<TransferToAccountUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToAccountEventReaderBuilder OnDeleted(Func<TransferToAccountDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ITransferToAccountEventReaderBuilder OnProvideRequired(Func<TransferToAccountProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}