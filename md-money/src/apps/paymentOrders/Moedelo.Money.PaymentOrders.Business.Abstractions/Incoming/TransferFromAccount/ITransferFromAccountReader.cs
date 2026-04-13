using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountReader
    {
        Task<PaymentOrderResponse> GetByBaseIdAsync(long documentBaseId);
    }
}