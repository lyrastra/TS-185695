using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining
{
    public interface ILoanObtainingValidator
    {
        Task ValidateAsync(LoanObtainingSaveRequest request);
    }
}
