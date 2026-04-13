using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportCurrencyPaymentFromCustomer commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyPaymentFromCustomer commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorCurrencyPaymentFromCustomer commandData);
        Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingExchangeRateCurrencyPaymentFromCustomer commandData);
    }
}