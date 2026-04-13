using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public interface ICurrencyPaymentToSupplierCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportCurrencyPaymentToSupplier commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyPaymentToSupplier commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingMissingContractorCurrencyPaymentToSupplier commandData);
        Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier commandData);
    }
}