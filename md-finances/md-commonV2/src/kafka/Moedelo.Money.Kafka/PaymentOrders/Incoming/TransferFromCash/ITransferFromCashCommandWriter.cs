using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromCash.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromCash
{
    public interface ITransferFromCashCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportTransferFromCash commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateTransferFromCash commandData);
    }
}