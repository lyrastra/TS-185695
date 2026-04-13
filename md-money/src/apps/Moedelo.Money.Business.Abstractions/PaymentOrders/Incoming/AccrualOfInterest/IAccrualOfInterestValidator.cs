using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestValidator
    {
        Task ValidateAsync(AccrualOfInterestSaveRequest request);
    }
}