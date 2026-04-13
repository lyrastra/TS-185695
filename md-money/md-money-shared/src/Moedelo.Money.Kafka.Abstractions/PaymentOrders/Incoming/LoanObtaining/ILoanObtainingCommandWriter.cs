using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining
{
    public interface ILoanObtainingCommandWriter
    {
        Task WriteImportAsync(ImportLoanObtaining commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateLoanObtaining commandData);

        Task WriteImportWithMissingContractAsync(ImportWithMissingContractLoanObtaining commandData);

        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorLoanObtaining commandData);
    }
}