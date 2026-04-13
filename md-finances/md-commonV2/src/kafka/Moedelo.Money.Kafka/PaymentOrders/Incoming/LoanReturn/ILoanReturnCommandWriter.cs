using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportLoanReturn commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateLoanReturn commandData);
        Task WriteImportWithMissingContractAsync(string key, string token, ImportWithMissingContractLoanReturn commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorLoanReturn commandData);
    }
}