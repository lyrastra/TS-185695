using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount
{
    public interface IRefundToSettlementAccountReader
    {
        Task<RefundToSettlementAccountResponse> GetByBaseIdAsync(long baseId);
    }
}
