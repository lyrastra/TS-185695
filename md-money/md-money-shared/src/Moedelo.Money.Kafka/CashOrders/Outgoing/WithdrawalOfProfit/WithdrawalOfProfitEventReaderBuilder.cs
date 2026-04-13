using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.WithdrawalOfProfit.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(IWithdrawalOfProfitEventReaderBuilder))]
    class WithdrawalOfProfitEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IWithdrawalOfProfitEventReaderBuilder
    {
        public WithdrawalOfProfitEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.WithdrawalOfProfit.Event.Topic,
                MoneyTopics.CashOrders.WithdrawalOfProfit.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IWithdrawalOfProfitEventReaderBuilder OnCreated(Func<WithdrawalOfProfitCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithdrawalOfProfitEventReaderBuilder OnUpdated(Func<WithdrawalOfProfitUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithdrawalOfProfitEventReaderBuilder OnDeleted(Func<WithdrawalOfProfitDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
