using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestReader
    {
        Task<AccrualOfInterestResponse> GetByBaseIdAsync(long baseId);
    }
}
