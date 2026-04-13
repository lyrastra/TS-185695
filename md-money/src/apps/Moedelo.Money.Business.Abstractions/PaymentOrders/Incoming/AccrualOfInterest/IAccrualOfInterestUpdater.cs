using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(AccrualOfInterestSaveRequest request);
    }
}