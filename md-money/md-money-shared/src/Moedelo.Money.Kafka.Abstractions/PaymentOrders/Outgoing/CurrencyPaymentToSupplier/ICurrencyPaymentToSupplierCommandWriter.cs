using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public interface ICurrencyPaymentToSupplierCommandWriter
    {
        Task WriteImportAsync(
            ImportCurrencyPaymentToSupplier commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateCurrencyPaymentToSupplier commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingMissingContractorCurrencyPaymentToSupplier commandData);

        Task WriteImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier commandData);
    }
}