using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining
{
    public interface ILoanObtainingReader
    {
        Task<LoanObtainingResponse> GetByBaseIdAsync(long baseId);
    }
}
