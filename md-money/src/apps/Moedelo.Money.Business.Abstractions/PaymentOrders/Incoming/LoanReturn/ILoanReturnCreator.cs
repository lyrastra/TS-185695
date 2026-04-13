using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(LoanReturnSaveRequest request);
    }
}