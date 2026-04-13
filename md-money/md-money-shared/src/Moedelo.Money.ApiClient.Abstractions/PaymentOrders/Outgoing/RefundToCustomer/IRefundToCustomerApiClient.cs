using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.RefundToCustomer
{
    public interface IRefundToCustomerApiClient
    {
        Task<RefundToCustomerPaymentDto> GetByIdAsync(long documentBaseId);
    }
}