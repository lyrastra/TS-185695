using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public interface ICurrencyTransferFromAccountCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportCurrencyTransferFromAccount commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyTransferFromAccount commandData);
        Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount commandData);
    }
}