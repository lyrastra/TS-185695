using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnCommandWriter
    {
        Task WriteImportAsync(ImportLoanReturn commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateLoanReturn commandData);

        Task WriteImportWithMissingContractAsync(ImportWithMissingContractLoanReturn commandData);

        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorLoanReturn commandData);
    }
}