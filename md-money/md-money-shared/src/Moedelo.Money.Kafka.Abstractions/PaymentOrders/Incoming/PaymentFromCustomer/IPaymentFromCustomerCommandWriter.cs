using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerCommandWriter
    {
        Task WriteImportAsync(
            ImportPaymentFromCustomer commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicatePaymentFromCustomer commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorPaymentFromCustomer commandData);
    }
}