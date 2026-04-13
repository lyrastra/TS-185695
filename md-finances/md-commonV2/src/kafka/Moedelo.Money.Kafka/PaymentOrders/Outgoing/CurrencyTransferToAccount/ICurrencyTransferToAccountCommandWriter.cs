using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public interface ICurrencyTransferToAccountCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportCurrencyTransferToAccount commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyTransferToAccount commandData);
        Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount commandData);
    }
}
