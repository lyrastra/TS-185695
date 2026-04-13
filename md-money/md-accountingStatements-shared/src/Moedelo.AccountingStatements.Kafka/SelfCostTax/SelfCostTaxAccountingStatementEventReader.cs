using System;
using System.Threading.Tasks;
using Moedelo.AccountingStatements.Kafka.Abstractions;
using Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax;
using Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax.Events;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.AccountingStatements.Kafka.SelfCostTax
{
    [InjectAsSingleton(typeof(ISelfCostTaxAccountingStatementEventReader))]
    internal sealed class SelfCostTaxAccountingStatementEventReader : MoedeloEntityEventKafkaTopicReaderBuilder, ISelfCostTaxAccountingStatementEventReader
    {
        public SelfCostTaxAccountingStatementEventReader(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  AccountingStatementsTopics.SelfCostTax.Event.Topic,
                  AccountingStatementsTopics.SelfCostTax.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ISelfCostTaxAccountingStatementEventReader OnMultipleCreated(Func<SelfCostTaxMultipleCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ISelfCostTaxAccountingStatementEventReader OnMultipleDeleted(Func<SelfCostTaxMultipleDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
