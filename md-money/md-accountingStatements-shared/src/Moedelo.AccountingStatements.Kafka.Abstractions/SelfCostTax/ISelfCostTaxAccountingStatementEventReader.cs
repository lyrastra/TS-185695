using System;
using System.Threading.Tasks;
using Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax
{
    public interface ISelfCostTaxAccountingStatementEventReader : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        ISelfCostTaxAccountingStatementEventReader OnMultipleCreated(Func<SelfCostTaxMultipleCreated, Task> onEvent);
        ISelfCostTaxAccountingStatementEventReader OnMultipleDeleted(Func<SelfCostTaxMultipleDeleted, Task> onEvent);
    }
}