using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeCommandWriter
    {
        Task WriteImportAsync(
            ImportCurrencyBankFee commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateCurrencyBankFee commandData);

        Task WriteImportWithMissingExchangeRateAsync(
            ImportWithMissingExchangeRateCurrencyBankFee commandData);
    }
}