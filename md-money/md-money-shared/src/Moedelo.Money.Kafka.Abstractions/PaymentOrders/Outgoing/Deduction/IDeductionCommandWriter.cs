using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction
{
    public interface IDeductionCommandWriter
    {
        Task WriteImportAsync(ImportDeduction commandData);

        Task WriteImportDuplicateAsync(ImportDeductionDuplicate commandData);

        Task WriteImportWithMissingContractorAsync(ImportDeductionWithMissingContractor commandData);
        
        Task WriteImportWithMissingEmployeeAsync(ImportDeductionWithMissingEmployee commandData);
    }
}
