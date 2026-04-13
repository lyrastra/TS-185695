using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public interface IIncomeFromCommissionAgentCommandWriter
    {
        Task WriteImportAsync(ImportIncomeFromCommissionAgent commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateIncomeFromCommissionAgent commandData);

        Task WriteImportWithMissingContractAsync(ImportWithMissingContractIncomeFromCommissionAgent commandData);

        Task WriteImportWithMissingComissionAgentAsync(ImportWithMissingContractorIncomeFromCommissionAgent commandData);
    }
}