using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment
{
    public interface IRentPaymentCommandWriter
    {
        Task WriteImportAsync(ImportRentPayment command);
        Task WriteImportWithMissingContractAsync(ImportWithMissingContractRentPayment command);
        Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorRentPayment command);
        Task WriteImportDuplicateAsync(ImportDuplicateRentPayment command);
    }
}