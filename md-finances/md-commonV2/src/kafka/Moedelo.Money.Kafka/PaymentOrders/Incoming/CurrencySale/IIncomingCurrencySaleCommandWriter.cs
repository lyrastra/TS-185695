using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencySale.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencySale
{
    public interface IIncomingCurrencySaleCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportIncomingCurrencySale commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateIncomingCurrencySale commandData);
        Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountIncomingCurrencySale commandData);
    }
}
