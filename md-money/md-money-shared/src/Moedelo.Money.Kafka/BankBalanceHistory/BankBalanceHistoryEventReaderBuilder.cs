using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.BankBalanceHistory;
using Moedelo.Money.Kafka.Abstractions.BankBalanceHistory.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.BankBalanceHistory
{
    [InjectAsSingleton(typeof(IBankBalanceHistoryEventReaderBuilder))]
    class BankBalanceHistoryEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IBankBalanceHistoryEventReaderBuilder
    {
        public BankBalanceHistoryEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.BankBalanceHistory.Movement.Event.Topic,
                MoneyTopics.BankBalanceHistory.Movement.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IBankBalanceHistoryEventReaderBuilder OnProcessed(Func<MovementProcessed, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
