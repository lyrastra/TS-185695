using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyBankFee.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportCurrencyBankFee commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyBankFee commandData);
        Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingExchangeRateCurrencyBankFee commandData);
    }
}