using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.PaymentFromCustomer.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportPaymentFromCustomer commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicatePaymentFromCustomer commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorPaymentFromCustomer commandData);
    }
}