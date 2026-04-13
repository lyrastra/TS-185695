using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentUpdater : IPaymentOrderUpdater<LoanRepaymentSaveRequest, PaymentOrderSaveResponse>
    {
    }
}