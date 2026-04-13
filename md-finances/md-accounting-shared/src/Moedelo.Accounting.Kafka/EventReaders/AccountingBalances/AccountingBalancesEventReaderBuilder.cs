using Moedelo.Accounting.Kafka.Abstractions.EventReaders;
using Moedelo.Accounting.Kafka.Abstractions.Events.EventData.AccountingBalances;
using Moedelo.Accounting.Kafka.Abstractions.Events.Topics.AccountingBalances;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;

namespace Moedelo.Accounting.Kafka.EventReaders.AccountingBalances
{
    [InjectAsSingleton(typeof(IAccountingBalancesEventReaderBuilder))]
    internal sealed class AccountingBalancesEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAccountingBalancesEventReaderBuilder
    {
        public AccountingBalancesEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                AccountingBalancesTopics.Event.Topic,
                AccountingBalancesTopics.Event.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IAccountingBalancesEventReaderBuilder OnMinDateChanged(Func<AccountingBalancesMinDateChangedMessage, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}