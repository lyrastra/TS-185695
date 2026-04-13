using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportWithdrawalOfProfit commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateWithdrawalOfProfit commandData);
        Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorWithdrawalOfProfit commandData);

    }
}