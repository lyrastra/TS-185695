using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;

public interface IRefundToSettlementAccountOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(RefundToSettlementAccountSaveRequest request);
}