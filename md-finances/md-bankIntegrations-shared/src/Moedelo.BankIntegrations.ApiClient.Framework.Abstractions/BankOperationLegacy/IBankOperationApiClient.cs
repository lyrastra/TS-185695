using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperationLegacy
{
    public interface IBankOperationApiClient
    {
        Task<SendPaymentOrderResponseDto> SendPaymentOrdersAsync(List<PaymentOrderDto> paymentOrders, IntegrationIdentityDto identity);
    }
}