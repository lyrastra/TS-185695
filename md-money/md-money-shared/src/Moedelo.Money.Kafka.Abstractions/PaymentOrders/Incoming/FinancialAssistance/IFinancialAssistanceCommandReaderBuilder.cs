using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance
{
    // note: Должен использоваться только в md-money!
    public interface IFinancialAssistanceCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IFinancialAssistanceCommandReaderBuilder OnImport(Func<ImportFinancialAssistance, Task> onCommand);
        IFinancialAssistanceCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateFinancialAssistance, Task> onCommand);
        IFinancialAssistanceCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorFinancialAssistance, Task> onCommand);
    }
}