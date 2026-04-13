using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest
{
    public interface IAccrualOfInterestProvider
    {
        Task ProvideAsync(long documentBaseId);
    }
}