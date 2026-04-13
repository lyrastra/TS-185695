using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail
{
    public interface IPaymentDetailApiClient
    {
        Task<PaymentDetailDto> GetAsync(long documentBaseId);
    }
}