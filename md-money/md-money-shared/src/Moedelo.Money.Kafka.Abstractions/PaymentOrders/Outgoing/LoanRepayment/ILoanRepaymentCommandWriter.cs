using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentCommandWriter
    {
        Task WriteImportAsync(ImportLoanRepayment commandData);
        
        Task WriteImportDuplicateAsync(ImportDuplicateLoanRepayment commandData);
        
        Task WriteImportWithMissingContractAsync(ImportWithMissingContractLoanRepayment commandData);
        
        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorLoanRepayment commandData);
    }
}