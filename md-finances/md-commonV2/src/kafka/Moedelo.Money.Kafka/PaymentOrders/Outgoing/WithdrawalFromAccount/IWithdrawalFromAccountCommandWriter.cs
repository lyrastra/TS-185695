using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportWithdrawalFromAccount commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateWithdrawalFromAccount commandData);
    }
}