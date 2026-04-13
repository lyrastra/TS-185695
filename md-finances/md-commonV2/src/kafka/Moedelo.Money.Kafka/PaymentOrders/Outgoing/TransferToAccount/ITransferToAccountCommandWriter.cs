using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.TransferToAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.TransferToAccount
{
    public interface ITransferToAccountCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportTransferToAccount commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateTransferToAccount commandData);
    }
}