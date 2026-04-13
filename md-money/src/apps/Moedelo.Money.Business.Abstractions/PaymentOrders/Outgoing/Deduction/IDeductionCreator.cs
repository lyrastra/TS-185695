using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction
{
    public interface IDeductionCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(DeductionSaveRequest request);
    }
}