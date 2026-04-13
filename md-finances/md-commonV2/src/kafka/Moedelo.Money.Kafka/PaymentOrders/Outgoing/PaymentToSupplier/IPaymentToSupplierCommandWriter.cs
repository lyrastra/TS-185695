using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToSupplier.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportPaymentToSupplier commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicatePaymentToSupplier commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorPaymentToSupplier commandData);
    }
}