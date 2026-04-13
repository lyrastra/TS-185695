using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanObtaining.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanObtaining
{
    public interface ILoanObtainingCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportLoanObtaining commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateLoanObtaining commandData);
        Task WriteImportWithMissingContractAsync(string key, string token, ImportWithMissingContractLoanObtaining commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorLoanObtaining commandData);
    }
}