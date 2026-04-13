using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromPurse.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromPurse
{
    public interface ITransferFromPurseCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportTransferFromPurse commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateTransferFromPurse commandData);
    }
}