using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportLoanIssue commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateLoanIssue commandData);
        Task WriteImportWithMissingContractAsync(string key, string token, ImportWithMissingContractLoanIssue commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorLoanIssue commandData);
    }
}