using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment
{
    public interface ILoanRepaymentValidator
    {
        Task ValidateAsync(LoanRepaymentSaveRequest request);
    }
}
