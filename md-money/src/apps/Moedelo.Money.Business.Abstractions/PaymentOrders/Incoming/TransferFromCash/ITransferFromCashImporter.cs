using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash
{
    public interface ITransferFromCashImporter
    {
        Task ImportAsync(TransferFromCashImportRequest request);
    }
}