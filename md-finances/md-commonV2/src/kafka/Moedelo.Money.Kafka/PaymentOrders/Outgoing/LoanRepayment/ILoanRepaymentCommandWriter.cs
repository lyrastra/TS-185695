using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanRepayment.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportLoanRepayment commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateLoanRepayment commandData);
        Task WriteImportWithMissingContractAsync(string key, string token, ImportWithMissingContractLoanRepayment commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorLoanRepayment commandData);
    }
}