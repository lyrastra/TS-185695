using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash
{
    public interface ITransferFromCashReader
    {
        Task<TransferFromCashResponse> GetByBaseIdAsync(long documentBaseId);
    }
}