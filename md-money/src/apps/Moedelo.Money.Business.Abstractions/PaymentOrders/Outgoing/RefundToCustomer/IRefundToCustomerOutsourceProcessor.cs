using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;

public interface IRefundToCustomerOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(RefundToCustomerSaveRequest request);
}