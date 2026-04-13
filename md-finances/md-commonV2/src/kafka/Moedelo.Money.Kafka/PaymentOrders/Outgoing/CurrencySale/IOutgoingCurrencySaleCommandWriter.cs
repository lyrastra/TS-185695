using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportOutgoingCurrencySale commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateOutgoingCurrencySale commandData);
        Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingMissingExchangeRateOutgoingCurrencySale commandData);
    }
}
