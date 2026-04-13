using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportTransferFromAccount commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateTransferFromAccount commandData);
    }
}