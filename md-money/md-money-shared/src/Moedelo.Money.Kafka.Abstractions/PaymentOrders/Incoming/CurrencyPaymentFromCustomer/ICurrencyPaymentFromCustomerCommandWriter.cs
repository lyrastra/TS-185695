using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public interface ICurrencyPaymentFromCustomerCommandWriter
    {
        Task WriteImportAsync(
            ImportCurrencyPaymentFromCustomer commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateCurrencyPaymentFromCustomer commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorCurrencyPaymentFromCustomer commandData);

        Task WriteImportWithMissingExchangeRateAsync(
            ImportWithMissingExchangeRateCurrencyPaymentFromCustomer commandData);
    }
}