using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnReader
    {
        Task<LoanReturnResponse> GetByBaseIdAsync(long baseId);
    }
}
