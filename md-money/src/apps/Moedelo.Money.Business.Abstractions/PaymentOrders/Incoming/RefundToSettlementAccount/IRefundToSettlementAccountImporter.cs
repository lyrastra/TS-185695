using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount
{
    public interface IRefundToSettlementAccountImporter
    {
        Task ImportAsync(ImportRefundToSettlementAccountRequest request);
    }
}
