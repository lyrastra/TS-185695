using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.PaymentOrder;

public interface IPaymentOrderClient
{
    Task<SendPaymentOrderResponseDto> SendPaymentOrder(SendPaymentOrderRequestDto data);
}