using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionOfOwnFunds.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(IContributionOfOwnFundsEventReaderBuilder))]
    class ContributionOfOwnFundsEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IContributionOfOwnFundsEventReaderBuilder
    {
        public ContributionOfOwnFundsEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.ContributionOfOwnFunds.Event.Topic,
                MoneyTopics.CashOrders.ContributionOfOwnFunds.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IContributionOfOwnFundsEventReaderBuilder OnCreated(Func<ContributionOfOwnFundsCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IContributionOfOwnFundsEventReaderBuilder OnUpdated(Func<ContributionOfOwnFundsUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IContributionOfOwnFundsEventReaderBuilder OnDeleted(Func<ContributionOfOwnFundsDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
