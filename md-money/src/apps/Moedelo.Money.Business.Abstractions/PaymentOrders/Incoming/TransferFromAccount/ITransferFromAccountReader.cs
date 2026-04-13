using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount
{
    public interface ITransferFromAccountReader
    {
        Task<TransferFromAccountResponse> GetByBaseIdAsync(long documentBaseId);
    }
}