using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public interface ICurrencyTransferFromAccountReader
    {
        Task<CurrencyTransferFromAccountResponse> GetByBaseIdAsync(long documentBaseId);
    }
}