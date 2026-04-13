using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(IWithdrawalFromAccountEventReaderBuilder))]
    public class WithdrawalFromAccountEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IWithdrawalFromAccountEventReaderBuilder
    {
        public WithdrawalFromAccountEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.WithdrawalFromAccount.Event.Topic,
                  MoneyTopics.PaymentOrders.WithdrawalFromAccount.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IWithdrawalFromAccountEventReaderBuilder OnCreated(Func<WithdrawalFromAccountCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithdrawalFromAccountEventReaderBuilder OnUpdated(Func<WithdrawalFromAccountUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithdrawalFromAccountEventReaderBuilder OnDeleted(Func<WithdrawalFromAccountDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithdrawalFromAccountEventReaderBuilder OnProvideRequired(Func<WithdrawalFromAccountProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}