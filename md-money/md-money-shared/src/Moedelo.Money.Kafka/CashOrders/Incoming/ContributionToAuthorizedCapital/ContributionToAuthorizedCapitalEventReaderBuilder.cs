using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.ContributionToAuthorizedCapital.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalEventReaderBuilder))]
    class ContributionToAuthorizedCapitalEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IContributionToAuthorizedCapitalEventReaderBuilder
    {
        public ContributionToAuthorizedCapitalEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.ContributionToAuthorizedCapital.Event.Topic,
                MoneyTopics.CashOrders.ContributionToAuthorizedCapital.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IContributionToAuthorizedCapitalEventReaderBuilder OnCreated(Func<ContributionToAuthorizedCapitalCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IContributionToAuthorizedCapitalEventReaderBuilder OnUpdated(Func<ContributionToAuthorizedCapitalUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IContributionToAuthorizedCapitalEventReaderBuilder OnDeleted(Func<ContributionToAuthorizedCapitalDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
