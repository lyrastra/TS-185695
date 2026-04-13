using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierCommandWriter
    {
        Task WriteImportAsync(
            ImportPaymentToSupplier commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicatePaymentToSupplier commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorPaymentToSupplier commandData);

        Task WriteApplyIgnoreNumberAsync(ApplyIgnoreNumberPaymentToSupplier commandData);
    }
}