using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    // note: Должен использоваться только в md-money!
    public interface IIncomeFromCommissionAgentCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IIncomeFromCommissionAgentCommandReaderBuilder OnImport(Func<ImportIncomeFromCommissionAgent, Task> onCommand);
        IIncomeFromCommissionAgentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateIncomeFromCommissionAgent, Task> onCommand);
        IIncomeFromCommissionAgentCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractIncomeFromCommissionAgent, Task> onCommand);
        IIncomeFromCommissionAgentCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorIncomeFromCommissionAgent, Task> onCommand);
    }
}