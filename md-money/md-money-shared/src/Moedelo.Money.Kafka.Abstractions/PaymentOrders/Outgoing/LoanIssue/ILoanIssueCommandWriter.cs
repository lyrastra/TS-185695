using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueCommandWriter
    {
        Task WriteImportAsync(ImportLoanIssue commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateLoanIssue commandData);

        Task WriteImportWithMissingContractAsync(ImportWithMissingContractLoanIssue commandData);

        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorLoanIssue commandData);
    }
}