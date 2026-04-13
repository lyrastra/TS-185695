using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount
{
    public interface IRefundToSettlementAccountCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(RefundToSettlementAccountSaveRequest request);
    }
}