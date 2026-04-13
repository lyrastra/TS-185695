using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.PaymentOrder;

public interface IPaymentOrderClient
{
    Task<SendPaymentOrderResponseDto> SendPaymentOrderAsync(SendPaymentOrderRequestDto data);
}