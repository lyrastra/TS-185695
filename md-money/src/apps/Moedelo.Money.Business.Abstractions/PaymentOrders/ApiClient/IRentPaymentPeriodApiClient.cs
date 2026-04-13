using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.ApiClient
{
    public interface IRentPaymentPeriodApiClient
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(TRequest data) where TRequest : class;
    }
}
